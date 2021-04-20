using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectsLib
{
    public class ParametersAndType : IEquatable<ParametersAndType>
    {
        public ParameterInfo[] Parameters;
        public Type ReturnType;

        public ParametersAndType(ParameterInfo[] parameterInfos, Type returnType)
        {
            if (parameterInfos == null) throw new ArgumentNullException();

            Parameters = new ParameterInfo[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Parameters[i] = parameterInfos[i];
            }
            ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        }

        public void Clear()
        {
            Parameters = null;
            ReturnType = null;
        }

        public bool Equals(ParametersAndType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Parameters, other.Parameters) && Equals(ReturnType, other.ReturnType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParametersAndType)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = 803715368;
            hashCode = hashCode * -1521134295 + EqualityComparer<ParameterInfo[]>.Default.GetHashCode(Parameters);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(ReturnType);
            return hashCode;
        }
    }
}
