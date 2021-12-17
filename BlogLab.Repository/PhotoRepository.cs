using BlogLab.Models.Photo;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConfiguration _config;

        public PhotoRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int photoid)
        {
            int affectedRows = 0;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync("Photo_Delete",
                    new { PhotoId = photoid }, commandType: CommandType.StoredProcedure);

            }
            return affectedRows;
        }

        public async Task<List<Photo>> GetAllByUserIdAsync(int applicationUserId)
        {
            IEnumerable<Photo> photos;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                photos = await connection.QueryAsync<Photo>("Photo_GetByUserId",
                    new { ApplicationUserId = applicationUserId }, commandType: CommandType.StoredProcedure);


            }
            return photos.ToList();

        }

        public async Task<Photo> GetAsync(int photoid)
        {
            Photo photo;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                photo = await connection.QueryFirstOrDefaultAsync<Photo>("Photo_Get",
                    new { PhotoId = photoid }, commandType: CommandType.StoredProcedure);


            }
            return photo;
        }

        public async Task<Photo> InsertAsync(PhotoCreate photocreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("PublicId", typeof(string));
            dataTable.Columns.Add("ImageUrl", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            dataTable.Rows.Add(photocreate.PublicId, photocreate.ImageUrl, photocreate.Description);

            int newPhotoId;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newPhotoId = await connection.ExecuteScalarAsync<int>("Photo_Insert",
                    new { Photo = dataTable.AsTableValuedParameter("dbo.PhotoType"),
                        ApplicationUserId = applicationUserId
                    }, commandType: CommandType.StoredProcedure);
            }
            Photo photo = await GetAsync(newPhotoId);
            return photo;
        }
        
    }
}
