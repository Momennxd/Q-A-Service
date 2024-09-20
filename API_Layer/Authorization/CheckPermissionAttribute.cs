namespace API_Layer.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckPermissionAttribute : Attribute
    {

        public CheckPermissionAttribute(Permissions permission)
        {
            Permission = permission;
        }

        public Permissions Permission { get; }

    }
}
