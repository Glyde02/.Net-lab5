using System;

namespace ClassesForTest
{
    public interface IService
    {
        void MyMethod();
    }

    public interface IGenericService<T>
    {
        void MyMethod();
    }

    public class GenericClass<T> : IService
    {
        public void MyMethod()
        {
            throw new NotImplementedException();
        }
    }

    public class ServiceImpl1 : IService
    {
        public void MyMethod()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class AbstractService
    {
        public abstract void MyMethod();
    }

    public class ServiceImpl2 : IService
    {
        public void MyMethod()
        {
            throw new NotImplementedException();
        }
    }

    public class AbstractServiceImpl : AbstractService
    {
        public override void MyMethod()
        {
            throw new NotImplementedException();
        }
    }

    public class GenericServiceImpl<T> : IGenericService<T>
    {
        public void MyMethod()
        {
            throw new NotImplementedException();
        }
    }

    public class Class1
    {

    }

    public class ClassWithDependencies
    {
        public IService Service;

        public IGenericService<int> GenericService;

        public ClassWithDependencies(IService service, IGenericService<int> genericService)
        {
            Service = service;
            GenericService = genericService;
        }
    }
}
