using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class dependencyProvider
    {
        private readonly Configuration config;

        public dependencyProvider(Configuration config)
        {
            this.config = config;
            config.IsUsed = true;
            foreach (var node in config.Map.Where(r => r.IsSingleton))
            {
                node.SingletonImplementation = Resolve(node.ImplementationType);
            }
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return ResolveAll(typeof(T)).Cast<T>();
        }

        private object Resolve(Type dependency)
        {
            return config.Map.Select(node => ResolveFromnode(node, dependency)).FirstOrDefault(result => result != null) ?? GenerateObject(dependency);
        }

        private IEnumerable<object> ResolveAll(Type dependency)
        {
            return config.Map.Select(node => ResolveFromnode(node, dependency)).Where(result => result != null);
        }

        private object ResolveFromnode(Node node, Type dependency)
        {
            if (dependency == node.DependencyType)
            {
                if (node.IsSingleton && node.SingletonImplementation != null)
                {
                    return node.SingletonImplementation;
                }
                return GenerateObject(node.ImplementationType);
            }
            if (node.DependencyType.IsGenericTypeDefinition && dependency.IsGenericType && dependency.GetGenericTypeDefinition() == node.DependencyType)
            {
                if (node.IsSingleton && node.SingletonImplementation != null)
                {
                    return node.SingletonImplementation;
                }
                return GenerateObject(node.ImplementationType.MakeGenericType(dependency.GetGenericArguments()));
            }

            return null;
        }

        private object GenerateObject(Type type)
        {
            var constructor = type.GetConstructors().Single();
            var parameters = constructor.GetParameters();
            return constructor.Invoke(parameters.Select(p => p.ParameterType).Select(Resolve).ToArray());
        }
    }
}
