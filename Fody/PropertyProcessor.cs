using System;
using System.Globalization;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using NullGuard;

public class PropertyProcessor
{
    public ModuleWeaver ModuleWeaver;
    public PropertyDefinition Property;
    private MethodBody getBody;
    private MethodBody setBody;

    public void Process()
    {
        try
        {
            InnerProcess();
        }
        catch (Exception exception)
        {
            throw new WeavingException(string.Format("An error occurred processing '{0}'. Error: {1}", Property.FullName, exception.Message));
        }
    }

    private void InnerProcess()
    {
        var validationFlags = this.ModuleWeaver.ValidationFlags;

        if (Property.DeclaringType.IsCustomAttributeDefined<NullGuardAttribute>())
        {
            var attribute = Property.DeclaringType.GetCustomAttribute<NullGuardAttribute>();
            if (attribute != null)
                validationFlags = (ValidationFlags)attribute.ConstructorArguments[0].Value;
        }

        if (!validationFlags.HasFlag(ValidationFlags.Properties)) return;

        if (Property.AllowsNull())
            return;

        getBody = Property.GetMethod.Body;
        getBody.SimplifyMacros();

        if ((validationFlags.HasFlag(ValidationFlags.NonPublic) || Property.GetMethod.IsPublic) &&
            !Property.GetMethod.MethodReturnType.AllowsNull() &&
            !Property.PropertyType.IsValueType)
        {
            InjectPropertyGetterGuard(validationFlags);
        }

        getBody.InitLocals = true;
        getBody.OptimizeMacros();

        setBody = Property.SetMethod.Body;
        setBody.SimplifyMacros();

        if (validationFlags.HasFlag(ValidationFlags.NonPublic) || Property.SetMethod.IsPublic)
        {
            InjectPropertySetterGuard(validationFlags);
        }

        setBody.InitLocals = true;
        setBody.OptimizeMacros();
    }

    private void InjectPropertyGetterGuard(ValidationFlags validationFlags)
    {
        var newInvalidOperationExceptionCtor = ModuleWeaver.ModuleDefinition.Import(typeof(InvalidOperationException).GetConstructor(new Type[] { typeof(string) }));

        var returnPoints = getBody.Instructions
            .Select((o, i) => new { o, i })
            .Where(a => a.o.OpCode == OpCodes.Ret)
            .Select(a => a.i)
            .OrderByDescending(i => i);

        foreach (var ret in returnPoints)
        {
            var returnInstruction = getBody.Instructions[ret];

            getBody.Instructions.Insert(ret,

                // Duplicate the stack (this should be the return value)
                Instruction.Create(OpCodes.Dup),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, returnInstruction),

                // Clean up the stack since we're about to throw up.
                Instruction.Create(OpCodes.Pop),

                // Load the exception text onto the stack
                Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Return value of property '{0}' is null.", Property.Name)),

                // Load the InvalidOperationException onto the stack
                Instruction.Create(OpCodes.Newobj, newInvalidOperationExceptionCtor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
                );
        }
    }

    private void InjectPropertySetterGuard(ValidationFlags validationFlags)
    {
        var newArgumentNullExceptionCtor = ModuleWeaver.ModuleDefinition.Import(typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string), typeof(string) }));

        var parameter = Property.SetMethod.Parameters[0]; // The Value parameter

        if (parameter.MayNotBeNull())
        {
            var entry = setBody.Instructions.First();

            setBody.Instructions.Prepend(

                // Load the argument onto the stack
                Instruction.Create(OpCodes.Ldarg, parameter),

                // Branch if value on stack is true, not null or non-zero
                Instruction.Create(OpCodes.Brtrue_S, entry),

                // Load the name of the argument onto the stack
                Instruction.Create(OpCodes.Ldstr, parameter.Name),

                // Load the exception text onto the stack
                Instruction.Create(OpCodes.Ldstr, String.Format(CultureInfo.InvariantCulture, "Cannot set the value of property '{0}' to null.", Property.Name)),

                // Load the ArgumentNullException onto the stack
                Instruction.Create(OpCodes.Newobj, newArgumentNullExceptionCtor),

                // Throw the top item of the stack
                Instruction.Create(OpCodes.Throw)
                );
        }
    }
}