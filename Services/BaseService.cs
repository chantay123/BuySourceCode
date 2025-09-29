using WebBuySource.Interfaces;

namespace WebBuySource.Services
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork">Unit of work</param>
    public abstract class BaseService(IUnitOfWork unitOfWork)
    {
        #region Reference services
        protected readonly IUnitOfWork UnitOfWork = unitOfWork;

        #endregion
        #region Constructor



        #endregion
    }
}
