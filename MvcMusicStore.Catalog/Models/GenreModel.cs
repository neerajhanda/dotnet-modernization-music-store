﻿using Amazon.DynamoDBv2.DataModel;
using System;

namespace MvcMusicStore.Catalog.Models
{
    // TODO: Map this to the appropriate DynamoDB table (single table design)
    [DynamoDBTable("Album")]
    public class GenreModel
    {
        private Guid? _genreId;

        /// <summary>
        /// DynamoDb Sort Key.
        /// Note: Album dynamodb table holds albums, artists and genres. Using generic sort key name.
        /// </summary>
        [DynamoDBRangeKey]
        public string SK { get; set; }

        public Guid GenreId
        {
            get => _genreId ?? Guid.Parse(SK.Replace("genre#", ""));
            set => _genreId = value;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
