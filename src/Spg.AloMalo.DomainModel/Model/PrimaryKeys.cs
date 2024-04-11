using Spg.AloMalo.DomainModel.Validators.RichTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    #region -- with class -------------------------------------------------------------------------

    //public class AlbumId : IComparable<AlbumId>, IEquatable<AlbumId>
    //{
    //    public int Value { get; }

    //    public AlbumId(int value)
    //    {
    //        Value = value;
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        return Equals(obj as AlbumId);
    //    }

    //    public bool Equals(AlbumId? other)
    //    {
    //        return Value.Equals(other?.Value);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return Value.GetHashCode();
    //    }

    //    public int CompareTo(AlbumId? other)
    //    {
    //        return Value.CompareTo(other?.Value);
    //    }

    //    public static implicit operator int(AlbumId beaconId)
    //    {
    //        return beaconId.Value;
    //    }
    //    public static implicit operator AlbumId(int id)
    //    {
    //        return new AlbumId(id);
    //    }

    //    public override string ToString() => Value.ToString();
    //}

    #endregion -- with class ----------------------------------------------------------------------

}
