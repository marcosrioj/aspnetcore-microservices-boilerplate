namespace MainProduct.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly MainProductDbContext _context;

        public InitialHostDbBuilder(MainProductDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // Create your Default Host Seed

            _context.SaveChanges();
        }
    }
}
