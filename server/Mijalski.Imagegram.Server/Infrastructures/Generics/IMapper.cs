namespace Mijalski.Imagegram.Server.Infrastructures.Generics;

interface IMapper<TEntity, TDbEntity>
{
    public TEntity Map(TDbEntity dbEntity);
    public TDbEntity Map(TEntity entity);
}