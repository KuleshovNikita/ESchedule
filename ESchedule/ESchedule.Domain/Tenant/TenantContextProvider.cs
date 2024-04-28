namespace ESchedule.Domain.Tenant
{
    public class TenantContextProvider : ITenantContextProvider
    {
        public TenantContext _current = null!;

        public TenantContext Current => _current;

        public void UseContext(TenantContext context)
        {
            if(_current != null)
            {
                return;
            }

            _current = context;
        }
    }
}
