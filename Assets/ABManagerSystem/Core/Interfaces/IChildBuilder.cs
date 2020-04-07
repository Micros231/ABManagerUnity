namespace ABManagerCore.Interfaces
{
    public interface IChildBuilder<TParent> 
    {

        TParent Complete();
    }
}

