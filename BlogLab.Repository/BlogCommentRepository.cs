﻿using BlogLab.Models.BlogComment;
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
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly IConfiguration _config;

        public BlogCommentRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<int> DeleteAsync(int blogCommentId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync(
                    "Blog_Comment_Delete",
                    new { BlogCommentId = blogCommentId },
                    commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }


        public async Task<List<BlogComment>> GetAllAsync(int blogId)
        {
            IEnumerable<BlogComment> blogComments;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                blogComments = await connection.QueryAsync<BlogComment>("Blog_Comment_GetAll",
                    new { BlogId = blogId }, commandType: CommandType.StoredProcedure);


            }
            return blogComments.ToList();
        }

        public async Task<BlogComment> GetAsync(int blogCommentId)
        {
            BlogComment blogComments;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                blogComments = await connection.QueryFirstOrDefaultAsync<BlogComment>("Blog_Comment_Get",
                    new { BlogCommentId = blogCommentId }, commandType: CommandType.StoredProcedure);


            }
            return blogComments;
        }

        public async Task<BlogComment> UpsertAsync(BlogCommentCreate blogCommentCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("BlogCommentId", typeof(int));
            dataTable.Columns.Add("ParentBlogCommentId", typeof(int));
            dataTable.Columns.Add("BlogId", typeof(int));
            dataTable.Columns.Add("Content", typeof(string));

            dataTable.Rows.Add(blogCommentCreate.BlogCommentId,
                blogCommentCreate.ParentBlogCommentId,
                blogCommentCreate.BlogId,
                blogCommentCreate.Content
                );

            int? newBlogCommentId;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newBlogCommentId = await connection.ExecuteScalarAsync<int>("Blog_Comment_Upsert",
                    new { BlogComment = dataTable.AsTableValuedParameter("dbo.BlogCommentType"),
                        ApplicationUserId = applicationUserId
                    }, commandType: CommandType.StoredProcedure);
            }

            newBlogCommentId = newBlogCommentId ?? blogCommentCreate.BlogCommentId;

            BlogComment blogComment = await GetAsync(newBlogCommentId.Value);
            return blogComment;
        }
    }
}
