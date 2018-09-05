using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Test.Helpers.Data
{
    public static partial class SetupData
    {
        public static List<OrgRole> _orgRoles = new List<OrgRole>()
        {
            //FleetCom
            new OrgRole {
                RoleOrderBy = 100,
                RoleName = "Fleet Commanding Officer",
                RoleShortName = "Fleet CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "FLEETCOM",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Admiral", RankType = "Navy" }
                }
            },
            new OrgRole {
                RoleOrderBy = 101,
                RoleName = "Fleet Executive Officer",
                RoleShortName = "Fleet XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "FLEETCOM",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Vice Admiral", RankType = "Navy" }
                }
            },
            new OrgRole {
                RoleOrderBy = 102,
                RoleName = "Chief of Naval Operations",
                RoleShortName = "CNO",
                RoleDisplayName = "Chief of Naval Operations",
                RoleType = "FLEETCOM",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Rear Admiral", RankType = "Navy" }
                }
            },
            new OrgRole {
                RoleOrderBy = 105,
                RoleName = "Fleet Senior Enlisted Adviser",
                RoleShortName = "Fleet Adviser",
                RoleDisplayName = "Senior Enlisted Adviser",
                RoleType = "FLEETCOM",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Fleet Master Chief Petty Officer", RankType = "Navy" }
                }
            },
            new OrgRole {
                RoleOrderBy = 106,
                RoleName = "Fleet Enlisted Adviser",
                RoleShortName = "Fleet Adviser",
                RoleDisplayName = "Enlisted Adviser",
                RoleType = "FLEETCOM",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Sergeant Major", RankType = "Marine" },
                }
            },

            //NAVCOM HQ
            new OrgRole {
                RoleOrderBy = 110,
                RoleName = "Naval Command Commanding Officer",
                RoleShortName = "NAVCOM CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "NAVCOM HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Rear Admiral (Lower Half)", RankType = "Navy" },
                    new Rank { RankName = "Brigadier General", RankType = "Marine" },
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Colonel", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 111,
                RoleName = "Naval Command Executive Officer",
                RoleShortName = "NAVCOM XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "NAVCOM HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Colonel", RankType = "Marine" },
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Colonel", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 112,
                RoleName = "Naval Command Senior Enlisted Adviser",
                RoleShortName = "NAVCOM Adviser",
                RoleDisplayName = "Senior Enlisted Adviser",
                RoleType = "NAVCOM HQ",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Sergeant Major", RankType = "Marine" },
                }
            },

            //Office Leadership
            new OrgRole {
                RoleOrderBy = 115,
                RoleName = "Senior Technology Officer",
                RoleShortName = "Senior Tech Officer",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 116,
                RoleName = "Senior Intelligence Officer",
                RoleShortName = "Senior Intel Officer",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 117,
                RoleName = "Senior Finance Officer",
                RoleShortName = "Senior Financier",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 118,
                RoleName = "Senior Public Relations Officer",
                RoleShortName = "Senior PR Officer",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 119,
                RoleName = "Senior Diplomacy Officer",
                RoleShortName = "Senior Diplomat",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                }
            },

            //AIR WING HQ
            new OrgRole {
                RoleOrderBy = 120,
                RoleName = "Air Wing Commanding Officer",
                RoleShortName = "Air Wing CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Air Wing HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 121,
                RoleName = "Air Wing Executive Officer",
                RoleShortName = "Air Wing XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "Air Wing HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 122,
                RoleName = "Air Wing Chief",
                RoleShortName = "Air Wing Chief",
                RoleType = "Air Wing HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Senior Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                }
            },

            //Combat Team HQ
            new OrgRole {
                RoleOrderBy = 130,
                RoleName = "Combat Team Commanding Officer",
                RoleShortName = "Combat Team CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Combat Team HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Lieutenant Colonel", RankType = "Marine" },
                    new Rank { RankName = "Major", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 131,
                RoleName = "Combat Team Executive Officer",
                RoleShortName = "Combat Team XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "Combat Team HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Major", RankType = "Marine" },
                    new Rank { RankName = "Captain", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 132,
                RoleName = "Combat Team Sergeant",
                RoleShortName = "Team Sergeant",
                RoleType = "Combat Team HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "First Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Gunnery Sergeant", RankType = "Marine" },
                }
            },

            //Squadron & Platoon Leadership
            new OrgRole {
                RoleOrderBy = 140,
                RoleName = "Squadron Leader",
                RoleType = "Squadron HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant (Junior Grade)", RankType = "Navy" },
                    new Rank { RankName = "Ensign", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 141,
                RoleName = "Platoon Leader",
                RoleType = "Platoon HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Captain", RankType = "Marine" },
                    new Rank { RankName = "First Lieutenant", RankType = "Marine" },
                    new Rank { RankName = "Second Lieutenant", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 141,
                RoleName = "Officer-in-Training",
                RoleShortName = "OIT",
                RoleType = "OIT",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Midshipman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 145,
                RoleName = "Squadron Chief",
                RoleShortName = "Squadron Chief",
                RoleType = "Squadron HQ",
                RatingCode = "AP",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 146,
                RoleName = "Platoon Sergeant",
                RoleShortName = "Platoon Sgt.",
                RoleType = "Platoon HQ",
                IsUnitLeadership = true,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Gunnery Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Staff Sergeant", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 147,
                RoleName = "Flight Leader",
                RoleType = "Tactical Flight Leadership",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 148,
                RoleName = "Crew Chief",
                RoleType = "Tactical Flight Leadership",
                RatingCode = "AW",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 148,
                RoleName = "Squad Leader",
                RoleType = "Tactical Marine Leadership",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Staff Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Sergeant", RankType = "Marine" },
                }
            },

            //Squadron Specialist & Support
            new OrgRole {
                RoleOrderBy = 149,
                RoleName = "Squadron Intelligence Specialist",
                RoleShortName = "Intel Specialist",
                RoleType = "Flight",
                RatingCode = "IS",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                }
            },

            //Aviation Roles
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Airlift Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Attack Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Combat Search & Rescue Operator",
                RoleShortName = "CSAR Operator",
                RoleType = "Flight",
                RatingCode = "AW",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Combat Search & Rescue Pilot",
                RoleShortName = "CSAR Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Combat Transport Pilot",
                RoleShortName = "ComTrans Operator",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Electronic Warfare Pilot",
                RoleShortName = "EWAR Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Electronic Warfare Technician",
                RoleShortName = "EWAR Tech",
                RoleType = "Flight",
                RatingCode = "ET",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Exploration Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Fighter Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Flight Engineer",
                RoleType = "Flight",
                RatingCode = "AD",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Loadmaster",
                RoleType = "Flight",
                RatingCode = "AW",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Operations Specialist",
                RoleShortName = "Ops Specialist",
                RoleType = "Flight",
                RatingCode = "AW",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Reconnaissance Operator",
                RoleShortName = "Recon Operator",
                RoleType = "Flight",
                RatingCode = "SO",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Strike Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 159,
                RoleName = "Flight Trainee",
                RoleType = "Flight",
                RatingCode = "AP",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            
            //Marine Roles
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Corpsman",
                RoleType = "Marine",
                RatingCode = "HM",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Demolitions Specialist",
                RoleShortName = "Demo Specialist",
                RoleType = "Marine",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Corporal", RankType = "Marine" },
                    new Rank { RankName = "Lance Corporal", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Heavy Weapons Operator",
                RoleShortName = "Weapons Operator",
                RoleType = "Marine",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Corporal", RankType = "Marine" },
                    new Rank { RankName = "Lance Corporal", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Marksman",
                RoleType = "Marine",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Corporal", RankType = "Marine" },
                    new Rank { RankName = "Lance Corporal", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Rifleman",
                RoleType = "Marine",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Sergeant", RankType = "Marine" },
                    new Rank { RankName = "Corporal", RankType = "Marine" },
                    new Rank { RankName = "Lance Corporal", RankType = "Marine" },
                }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Infantry Trainee",
                RoleType = "Marine",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Lance Corporal", RankType = "Marine" },
                }
            },

            //Office Roles - Officers
            new OrgRole {
                RoleOrderBy = 170,
                RoleName = "Technology Officer",
                RoleShortName = "Tech Officer",
                RoleType = "Office",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Admiral", RankType = "Navy" },
                    new Rank { RankName = "Vice Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral (Lower Half)", RankType = "Navy" },
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant (Junior Grade)", RankType = "Navy" },
                    new Rank { RankName = "Ensign", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 171,
                RoleName = "Intelligence Officer",
                RoleShortName = "Intel Officer",
                RoleType = "Office",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Admiral", RankType = "Navy" },
                    new Rank { RankName = "Vice Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral (Lower Half)", RankType = "Navy" },
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant (Junior Grade)", RankType = "Navy" },
                    new Rank { RankName = "Ensign", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 172,
                RoleName = "Finance Officer",
                RoleShortName = "Financier",
                RoleType = "Office",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Admiral", RankType = "Navy" },
                    new Rank { RankName = "Vice Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral (Lower Half)", RankType = "Navy" },
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant (Junior Grade)", RankType = "Navy" },
                    new Rank { RankName = "Ensign", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 173,
                RoleName = "Public Relations Officer",
                RoleShortName = "PR Officer",
                RoleType = "Office",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Admiral", RankType = "Navy" },
                    new Rank { RankName = "Vice Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral (Lower Half)", RankType = "Navy" },
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant (Junior Grade)", RankType = "Navy" },
                    new Rank { RankName = "Ensign", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 174,
                RoleName = "Diplomacy Officer",
                RoleShortName = "Diplomat",
                RoleType = "Office",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Admiral", RankType = "Navy" },
                    new Rank { RankName = "Vice Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral", RankType = "Navy" },
                    new Rank { RankName = "Rear Admiral (Lower Half)", RankType = "Navy" },
                    new Rank { RankName = "Captain", RankType = "Navy" },
                    new Rank { RankName = "Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant Commander", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant", RankType = "Navy" },
                    new Rank { RankName = "Lieutenant (Junior Grade)", RankType = "Navy" },
                    new Rank { RankName = "Ensign", RankType = "Navy" },
                }
            },

            //Office Roles - Enlisted
            new OrgRole {
                RoleOrderBy = 175,
                RoleName = "Technology Specialist",
                RoleShortName = "Tech Specialist",
                RoleType = "Office",
                RatingCode = "IT",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Senior Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 176,
                RoleName = "Intelligence Specialist",
                RoleShortName = "Intel Specialist",
                RoleType = "Office",
                RatingCode = "IS",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Senior Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 177,
                RoleName = "Finance Specialist",
                RoleShortName = "Finance  Spc.",
                RoleType = "Office",
                RatingCode = "YF",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Senior Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 178,
                RoleName = "Public Relations Specialist",
                RoleShortName = "PR  Specialist",
                RoleType = "Office",
                RatingCode = "MC",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Senior Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },
            new OrgRole {
                RoleOrderBy = 179,
                RoleName = "Diplomacy Specialist",
                RoleShortName = "Diplomacy Spc.",
                RoleType = "Office",
                RatingCode = "YD",
                IsUnitLeadership = false,
                SupportedRanks = new List<Rank>{
                    new Rank { RankName = "Master Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Senior Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Chief Petty Officer", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 1st Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 2nd Class", RankType = "Navy" },
                    new Rank { RankName = "Petty Officer 3rd Class", RankType = "Navy" },
                    new Rank { RankName = "Airman", RankType = "Navy" },
                }
            },

        };
        
    }
}
