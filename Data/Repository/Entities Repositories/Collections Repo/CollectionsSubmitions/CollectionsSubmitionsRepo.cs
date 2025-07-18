﻿using Data.DatabaseContext;
using Data.models.Collections;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.CollectionsSubmitions
{
    public class CollectionsSubmitionsRepo: Repository<Collections_Submitions>, ICollectionsSubmitionsRepo
    {
        AppDbContext _appDbContext;

        public CollectionsSubmitionsRepo(AppDbContext appDbContext)
            :base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Collections_Submitions> AddItemAsync(int CollectionID, int UserID)
        {
            var entity = new Collections_Submitions() { CollectionID = CollectionID, SubmittedUserID = UserID };
            await base.AddItemAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteSubmition(int submitionID, int userID)
        {
            var successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteSubmition @SubmitionID, @UserID, @Success OUTPUT",
                new SqlParameter("@SubmitionID", submitionID),
                new SqlParameter("@UserID", userID),
                successParam
            );

            return (bool)successParam.Value;
        }

        public async Task<List<Collections_Submitions>> GetSubmitions(int collectionID, int UserID)
        {
            return await _appDbContext.Collections_Submitions
                .Where(cs => cs.CollectionID == collectionID && cs.SubmittedUserID == UserID)
                .ToListAsync();
        }

    }
}
