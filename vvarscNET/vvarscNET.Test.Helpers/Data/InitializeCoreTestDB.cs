﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using vvarscNET.Core;
using vvarscNET.Core.Factories;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Core.Data.QueryHandlers.Authentication;
using vvarscNET.Core.CommandModels.Authentication;
using vvarscNET.Core.Data.CommandHandlers.Authentication;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Data.QueryHandlers.Organizations;
using vvarscNET.Core.CommandModels.Organizations;
using vvarscNET.Core.Data.CommandHandlers.Organizations;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Data.QueryHandlers.People;
using vvarscNET.Core.CommandModels.People;
using vvarscNET.Core.Data.CommandHandlers.People;
using vvarscNET.Model.Enums;

namespace vvarscNET.Test.Helpers.Data
{
    public class InitializeCoreTestDb
    {
        public static void Initialize(string connectionString, bool isTest = false)
        {
            if (!InitOrganization(connectionString))
                throw new Exception("Could not init organization");
            if (!InitOrgAdmins(connectionString))
                throw new Exception("Could not init organization admins");
            if (!InitSuperAdmin(connectionString))
                throw new Exception("Could not init superadmin");
            if (!InitPayGradesAndRanks(connectionString))
                throw new Exception("Could not init PayGrades and Ranks");
            if (!InitOrgRoles(connectionString))
                throw new Exception("Could not init OrgRoles");
            if (!InitUnits(connectionString))
                throw new Exception("Could not init Units");
        }

        #region SuperAdmin
        private static bool InitSuperAdmin(string connectionString)
        {
            Console.WriteLine("Init SuperAdmin");
            //Create Member
            string userName = "superadmin";
            var memCmd = new CreateMember_C
            {
                UserName = userName,
                RSIHandle = "invalid_handle",
                UserType = "SuperAdmin",
                OrganizationID = null,
                IsActive = true
            };

            CreateMember_CH createMember_CH = new CreateMember_CH(new SQLConnectionFactory(connectionString));
            var memResult = createMember_CH.Handle(Globals.UserContext, memCmd);

            if (memResult.Status != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            var memID = Convert.ToInt32(memResult.ItemIDs.FirstOrDefault());

            //Create Credential for Member
            SHA256Managed hashalgo = new SHA256Managed();
            byte[] hash = hashalgo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userName.ToLower()));
            string passwordHash = BitConverter.ToString(hash);
            passwordHash = passwordHash.Replace("-", "");

            var credCmd = new CreateCredential_C
            {
                MemberID = memID,
                UserName = userName,
                PasswordHash = passwordHash,
                OrganizationID = null
            };

            CreateCredential_CH createCredential_CH = new CreateCredential_CH(new SQLConnectionFactory(connectionString));
            var credResult = createCredential_CH.Handle(Globals.UserContext, credCmd);

            if (credResult.Status != System.Net.HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Organizations
        private static bool InitOrganization(string connectionString)
        {
            Console.WriteLine("Init Organizations");
            foreach (var t in SetupData._organizations)
            {
                var qry = new GetOrganizationBySpectrumID_Q
                {
                    SpectrumID = t.OrganizationSpectrumID
                };

                GetOrganizationBySpectrumID_QH getOrganizationBySpectrumID_QH = new GetOrganizationBySpectrumID_QH(new Core.Factories.SQLConnectionFactory(connectionString));
                var organization = getOrganizationBySpectrumID_QH.Handle(Globals.AuthHandlerToken, qry);

                if (organization == null)
                {
                    organization = Helpers.Clone(t);

                    var cmd = new CreateOrganization_C
                    {
                        OrganizationName = organization.OrganizationName,
                        OrganizationSpectrumID = organization.OrganizationSpectrumID,
                        OrganizationWebsiteURL = organization.OrganizationWebsiteURL,
                        IsActive = organization.IsActive
                    };

                    CreateOrganization_CH createOrganziationCommandHandler = new CreateOrganization_CH(new Core.Factories.SQLConnectionFactory(connectionString));
                    var cmdResult = createOrganziationCommandHandler.Handle(Globals.UserContext, cmd);

                    if (cmdResult.Status != System.Net.HttpStatusCode.OK)
                        return false;
                }
            }
            return true;
        }

        private static bool InitOrgAdmins(string connectionString)
        {
            Console.WriteLine("Init OrgAdmins");
            foreach (var org in SetupData._organizations)
            {
                //Setup AdminUser for Each Organization
                var qry = new GetOrganizationBySpectrumID_Q
                {
                    SpectrumID = org.OrganizationSpectrumID
                };

                GetOrganizationBySpectrumID_QH getOrganizationBySpectrumID_QH = new GetOrganizationBySpectrumID_QH(new SQLConnectionFactory(connectionString));
                var organization = getOrganizationBySpectrumID_QH.Handle(Globals.AuthHandlerToken, qry);

                if (organization != null)
                {

                    //Create Member
                    string userName = organization.OrganizationSpectrumID.ToLower() + ".admin";
                    var memCmd = new CreateMember_C
                    {
                        UserName = userName,
                        RSIHandle = "invalid_handle",
                        UserType = "Admin",
                        OrganizationID = organization.ID,
                        IsActive = true
                    };

                    CreateMember_CH createMember_CH = new CreateMember_CH(new SQLConnectionFactory(connectionString));
                    var memResult = createMember_CH.Handle(Globals.UserContext, memCmd);

                    if (memResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                    var memID = Convert.ToInt32(memResult.ItemIDs.FirstOrDefault());

                    //Create Credential for Member
                    SHA256Managed hashalgo = new SHA256Managed();
                    byte[] hash = hashalgo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userName.ToLower()));
                    string passwordHash = BitConverter.ToString(hash);
                    passwordHash = passwordHash.Replace("-", "");

                    var credCmd = new CreateCredential_C
                    {
                        MemberID = memID,
                        UserName = userName,
                        PasswordHash = passwordHash,
                        OrganizationID = organization.ID
                    };

                    CreateCredential_CH createCredential_CH = new CreateCredential_CH(new SQLConnectionFactory(connectionString));
                    var credResult = createCredential_CH.Handle(Globals.UserContext, credCmd);

                    if (credResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region OrgRoles
        private static bool InitOrgRoles(string connectionString)
        {
            Console.WriteLine("Init OrgRoles");

            //List Ranks
            ListRanks_QH listRanks_QH = new ListRanks_QH(new SQLConnectionFactory(connectionString));
            var _ranks = listRanks_QH.Handle(Globals.AuthHandlerToken, new ListRanks_Q());

            foreach (var or in SetupData._orgRoles)
            {
                //Create OrgRole
                var orCmd = new CreateOrgRole_C
                {
                    OrganizationID = 1,
                    RoleName = or.RoleName,
                    RoleShortName = or.RoleShortName,
                    RoleDisplayName = or.RoleDisplayName,
                    RoleType = or.RoleType,
                    RoleOrderBy = or.RoleOrderBy,
                    RatingCode = or.RatingCode,
                    IsActive = true,
                    IsHidden = or.IsHidden
                };

                CreateOrgRole_CH createOrgRole_CH = new CreateOrgRole_CH(new SQLConnectionFactory(connectionString));
                var pgResult = createOrgRole_CH.Handle(Globals.UserContext, orCmd);

                if (pgResult.Status != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var roleID = Convert.ToInt32(pgResult.ItemIDs.FirstOrDefault());

                //Map Ranks to OrgRole
                if (or.SupportedRanks != null && or.SupportedRanks.Count > 0)
                {
                    var rCmd = new InitRanksForOrgRole_C
                    {
                        OrgRoleID = roleID,
                        SupportedRanks = new List<int>()
                    };

                    foreach (var r in or.SupportedRanks)
                    {
                        //find this rank in full list
                        var localRank = _ranks.Where(a => a.RankName == r.RankName && a.RankType == r.RankType).FirstOrDefault();

                        //add to local cmd
                        if (localRank != null)
                            rCmd.SupportedRanks.Add(localRank.RankID);
                        else
                            throw new Exception("Error Mapping Ranks to OrgRoles in InitializeCoreTestDB");
                    }

                    InitRanksForOrgRole_CH initPayGradesForOrgRole_CH = new InitRanksForOrgRole_CH(new SQLConnectionFactory(connectionString));
                    var pgrResult = initPayGradesForOrgRole_CH.Handle(Globals.UserContext, rCmd);

                    if (pgrResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region PayGrades/Ranks
        private static bool InitPayGradesAndRanks(string connectionString)
        {
            Console.WriteLine("Init PayGradesAndRanks");
            foreach (var pg in SetupData._paygrades)
            {
                //Create PayGrade
                var pgCmd = new CreatePayGrade_C
                {
                    PayGradeName = pg.PayGradeName,
                    PayGradeDisplayName = pg.PayGradeDisplayName,
                    PayGradeOrderBy = pg.PayGradeOrderBy,
                    PayGradeGroup = pg.PayGradeGroup,
                    PayGradeDescriptionText = pg.PayGradeDescriptionText,
                    PayGradeNotes = pg.PayGradeNotes,
                    IsActive = true
                };

                CreatePayGrade_CH createPayGrade_CH = new CreatePayGrade_CH(new SQLConnectionFactory(connectionString));
                var pgResult = createPayGrade_CH.Handle(Globals.UserContext, pgCmd);

                if (pgResult.Status != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var pgID = Convert.ToInt32(pgResult.ItemIDs.FirstOrDefault());

                var pgRanks = SetupData._ranks.Where(a => a.PayGradeID == pgID).ToList();
                foreach (var rank in pgRanks)
                {
                    //Create Rank(s) for this PayGrade
                    var rankCmd = new CreateRank_C
                    {
                        PayGradeID = pgID,
                        RankName = rank.RankName,
                        RankAbbr = rank.RankAbbr,
                        RankType = rank.RankType,
                        RankImage = rank.RankImage,
                        RankGroupName = rank.RankGroupName,
                        RankGroupImage = rank.RankGroupImage,
                        RatingCodeSuffix = rank.RatingCodeSuffix,
                        IsActive = true
                    };

                    CreateRank_CH createRank_CH = new CreateRank_CH(new SQLConnectionFactory(connectionString));
                    var rankResult = createRank_CH.Handle(Globals.UserContext, rankCmd);

                    if (rankResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Units
        private static bool InitUnits(string connectionString)
        {
            Console.WriteLine("Init Units");
            foreach (var u in SetupData._units)
            {
                //Create Unit
                var unitCmd = new CreateUnit_C
                {
                    ParentUnitName = u.ParentUnitName,
                    UnitName = u.UnitName,
                    UnitFullName = u.UnitFullName,
                    UnitDesignation = u.UnitDesignation,
                    UnitDescription = u.UnitDescription,
                    UnitCallsign = u.UnitCallsign,
                    UnitType = u.UnitType.ToString(),
                    IsHidden = u.IsHidden,
                    IsActive = u.IsActive
                };

                CreateUnit_CH createUnit_CH = new CreateUnit_CH(new SQLConnectionFactory(connectionString));
                var unitResult = createUnit_CH.Handle(Globals.UserContext, unitCmd);

                if (unitResult.Status != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var unitID = Convert.ToInt32(unitResult.ItemIDs.FirstOrDefault());
            }
            return true;
        }

        #endregion
    }
}
