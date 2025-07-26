using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Application.Framework;

namespace InMemoryDb.DataEntity;

public record ProductEntity : BaseEntity<Guid>
    {
        public int ProductId { get; set; }
        [Updateable]
        public string ProductName { get; set; }
        [Updateable]
        public string ProductCode { get; set; }
        internal string _tagList { get; set; }
        [NotMapped, Updateable]
        public string[]? TagList
        {
        get
        {
            if (!string.IsNullOrEmpty(_tagList))
                return JsonConvert.DeserializeObject<string[]>(_tagList);
            else
                return null;
        }
        set
        {
            _tagList = String.Empty;
            if (value != null)
                _tagList = JsonConvert.SerializeObject(value);
        }
    }
        [Updateable]
        public DateTime ReleaseDate { get; set; }
        [Updateable]
        public double Price { get; set; }
        [Updateable]
        public string Description { get; set; }
        [Updateable]
        public double StarRating { get; set; }
        [Updateable]
        public string ImageUrl { get; set; }
    }