using BankingApp.DAL.Abstractions;

namespace Services
{
    public class BaseService
    {
        protected IRepositoryWrapper RepositoryWrapper;

        public BaseService(IRepositoryWrapper repositoryWrapper)
        {
            RepositoryWrapper = repositoryWrapper;
        }

        public void Save()
        {
            RepositoryWrapper.Save();
        }
    }
}
