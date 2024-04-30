using Spg.AloMalo.DomainModel.Validators.RichTypes;

namespace Spg.AloMalo.DomainModel.Model
{
    public record AlbumId(int Value) : IRichType<int>
    { }

    public record PhotographerId(int Value) : IRichType<int>
    { }

    public record PersonId(int Value) : IRichType<int>
    { }

    public record PhotoId(int Value) : IRichType<int>
    { }

    public record AlbumPhotoId(int Value) : IRichType<int>
    { }
}
