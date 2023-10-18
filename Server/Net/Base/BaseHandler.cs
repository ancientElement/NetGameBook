namespace AE_NetWork
{
    public abstract class BaseHandler
    {
        public BaseMessage message;

        public abstract void Handle(object obj);
    }
}