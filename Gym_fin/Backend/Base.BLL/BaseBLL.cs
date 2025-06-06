using Base.DAL.Contracts;

namespace Base.BLL.Contracts;

public class BaseBLL<TUOW> : IBaseBLL
    where TUOW : IBaseUOW
{
    protected readonly TUOW BLLUOW;

    public BaseBLL(TUOW uow)
    {
        BLLUOW = uow;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await BLLUOW.SaveChangesAsync();
    }
}