﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon;
using MvcMusicStore.CatalogApi.Models;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace MvcMusicStore.CatalogApi
{
    /**
     * TODO - These methods are just stubs and should be re-mapped 
     **/
    public class CatalogService
    {
        DynamoDBContext context;
        AmazonDynamoDBClient dynamoClient;

        public CatalogService()
        {
            dynamoClient = new AmazonDynamoDBClient();
            context = new DynamoDBContext(dynamoClient);
        }

        public async Task<IEnumerable<GenreModel>> Genres()
        {
            var queryOperation = context.QueryAsync<GenreModel>("GENRE");

            return await queryOperation.GetRemainingAsync();
        }

        public async Task<GenreModel> GenreByName(string name)
        {
            return await context.LoadAsync<GenreModel>("GENRE", name);
        }

        public async Task<AlbumModel> AlbumById(string albumId)
        {
            var queryOperation = context.QueryAsync<AlbumModel>(albumId, new DynamoDBOperationConfig { IndexName = "album-by-id" });

            var albums = await queryOperation.GetRemainingAsync();

            return albums.FirstOrDefault();

        }

        public async Task<IEnumerable<AlbumModel>> AlbumsByGenre(string genreName)
        {
            var queryOperation = context.QueryAsync<AlbumModel>(genreName, new DynamoDBOperationConfig { IndexName = "genre-albums" });

            return await queryOperation.GetRemainingAsync();
        }

        public async Task<IEnumerable<AlbumModel>> AlbumsByIdList(IEnumerable<string> ids)
        {
            var albums = new List<AlbumModel>();

            foreach (string albumId in ids)
            {
                albums.Add(await AlbumById(albumId));
            }
            
            return albums;
        }

    }
}