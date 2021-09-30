namespace Mijalski.Imagegram.Server.Infrastructures.Generics;

public interface ICreationAudited
{
    DateTimeOffset CreationDateTime { get; set; }
}