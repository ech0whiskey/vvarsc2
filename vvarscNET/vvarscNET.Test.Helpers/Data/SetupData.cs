using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Test.Helpers.Data
{
    public static class SetupData
    {
        #region Organizations
        public static List<Organization> _organizations = new List<Organization>()
        {
            new Organization
            {
                OrganizationName = "VVarMachine Navy",
                OrganizationSpectrumID = "VVAR",
                OrganizationWebsiteURL = "http://www.vvarmachine.com",
                IsActive = true
            }
        };

        #endregion

        #region PayGrades/Ranks
        public static List<PayGrade> _paygrades = new List<PayGrade>()
        {
            //Flag Officers
            new PayGrade {
                ID = 1,
                PayGradeName = "O-10",
                PayGradeDisplayName = "O-10",
                PayGradeOrderBy = 1,
                PayGradeGroup = "Flag Officer",
                PayGradeDescriptionText = "O-10 is highest rank in the Fleet, held only by the Fleet's Commanding Officer.",
                IsActive = true
            },
            new PayGrade {
                ID = 2,
                PayGradeName = "O-9",
                PayGradeDisplayName = "O-9",
                PayGradeOrderBy = 2,
                PayGradeGroup = "Flag Officer",
                PayGradeDescriptionText = "O-9 is the second-highest Flag Officer rank, held by the Fleet Executive Officer.",
                IsActive = true
            },
            new PayGrade {
                ID = 3,
                PayGradeName = "O-8",
                PayGradeDisplayName = "O-8",
                PayGradeOrderBy = 3,
                PayGradeGroup = "Flag Officer",
                PayGradeDescriptionText = "O-8 is the second of the Flag Officer Ranks, and is the PayGrade held by the Military Division Commanding Officer (DCOM).",
                IsActive = true
            },
            new PayGrade {
                ID = 4,
                PayGradeName = "O-7",
                PayGradeDisplayName = "O-7",
                PayGradeOrderBy = 4,
                PayGradeGroup = "Flag Officer",
                PayGradeDescriptionText = "O-7 is the first of the Flag Officer Ranks, held by the Economy Division Commanding Officer (DCOE), and is the 2nd of the PayGrades that can be granted to the Military Division Executive Officer.",
                IsActive = true
            },
            //Senior Officers
            new PayGrade {
                ID = 5,
                PayGradeName = "O-6",
                PayGradeDisplayName = "O-6",
                PayGradeOrderBy = 5,
                PayGradeGroup = "Senior Officer",
                PayGradeDescriptionText = "O-6 is the highest of the Senior Officer ranks, held by the most-senior Warfare Command COs, and also grants the ability for a Military Division Officer to serve as the Division's XO. On the Economy-side, this is the higher of the two ranks mapped to the XO position. These members have attained expert qualifications in multiple areas of responsibility, and are highly involved in Strategic Planning for their Division and the Fleet.",
                IsActive = true
            },
            new PayGrade {
                ID = 6,
                PayGradeName = "O-5",
                PayGradeDisplayName = "O-5",
                PayGradeOrderBy = 6,
                PayGradeGroup = "Senior Officer",
                PayGradeDescriptionText = "O-5 is the second of the Senior Officer Ranks. Officers in this grade can be assigned as the CO for a Warfare Command, and as Executive Officer of the Economy Division. Additionally, O-5s can serve as the XO for a Warfare Command when the CO is promoted to O-6. This is the highest PayGrade that can be held by Officers in Command of an Office or Department within the Fleet as their Primary Role.",
                IsActive = true
            },
            new PayGrade {
                ID = 7,
                PayGradeName = "O-4",
                PayGradeDisplayName = "O-4",
                PayGradeOrderBy = 7,
                PayGradeGroup = "Senior Officer",
                PayGradeDescriptionText = "O-4 is the first of the Senior Officer ranks, and is held by the most senior Air Wing and Combat Team Commanders, along with the Executive Officers of Warfare Commands. In addition to the above, O-4s can be placed in-charge of a Division or Fleet Office/Department such as Public Relations, Intelligence, Technology, etc.",
                IsActive = true
            },
            //Officers
            new PayGrade {
                ID = 8,
                PayGradeName = "O-3",
                PayGradeDisplayName = "O-3",
                PayGradeOrderBy = 8,
                PayGradeGroup = "Officer",
                PayGradeDescriptionText = "The PayGrade of O-3 grants the ability for an Officer to be assigned as the Commander of an Air Wing, or a Marine Combat Team. O-3 may also be held by senior Squadron/Platoon Commanders who are in-holding for an Air Wing or Combat Team CO position when it becomes available.",
                IsActive = true
            },
            new PayGrade {
                ID = 9,
                PayGradeName = "O-2",
                PayGradeDisplayName = "O-2",
                PayGradeOrderBy = 9,
                PayGradeGroup = "Officer",
                PayGradeDescriptionText = "O-2s are decorated and experienced veterans of leading Squadrons or Platoons for organized operations. These members have displayed consistent success in Tactical Planning, creation and maintenance of Unit SOPs, and adapting operational methodologies to an ever-changing mission environment.",
                IsActive = true
            },
            new PayGrade {
                ID = 10,
                PayGradeName = "O-1",
                PayGradeDisplayName = "O-1",
                PayGradeOrderBy = 10,
                PayGradeGroup = "Officer",
                PayGradeDescriptionText = "O-1s are fully-commissioned Officers; proficient and qualified Squadron/Platoon Leaders. They have successfully passed their OIT phase, and have taken-on the responsibility of leading and managing their unit in all regards. O-1s also gain the ability to create and manage Operational Templates and Mission Plans for the Fleet's activities during the operational planning phase.",
                IsActive = true
            },
            //Officer Candidate
            new PayGrade {
                ID = 11,
                PayGradeName = "OC",
                PayGradeDisplayName = "OC",
                PayGradeOrderBy = 11,
                PayGradeGroup = "Officer Candidate",
                PayGradeDescriptionText = "MIDN (OC) is the PayGrade held by Officer Candidates during their time in the Officer-in-Training Program. For Military Officers, this will be accompanied by an assignment to a permanent Squadron or Platoon to serve as an Assistant to the CO, much like an apprenticeship. Once the new Officer has attained the skills needed to lead a unit of their own, they will be advanced to O-1 and assigned as the CO of a Squadron or Platoon. For Economy Officers, no unit-assignment is granted, as there are no permanent units within the Division. OCs are expected to manage all aspects of their Unit's development under the guidance of the CO; operational planning, personnel training, unit public relations, recruitment, and member evaluation.",
                IsActive = true
            },
            //Senior NCOs
            new PayGrade {
                ID = 12,
                PayGradeName = "E-9 (Special)",
                PayGradeDisplayName = "E-9",
                PayGradeOrderBy = 12,
                PayGradeGroup = "Senior NCO",
                PayGradeDescriptionText = "This is a special variant of the E-9 PayGrade, held by the most-senior Enlisted Member of the Fleet.",
                IsActive = true
            },
            new PayGrade {
                ID = 13,
                PayGradeName = "E-9",
                PayGradeDisplayName = "E-9",
                PayGradeOrderBy = 13,
                PayGradeGroup = "Senior NCO",
                PayGradeDescriptionText = "E-9s are the highest-ranking Enlisted personnel within the Fleet, and serve as senior advisers to both the Division Command Staff, and Fleet Command Staff. E-9s have proven their strength in leadership, conflict resolution, and strategic planning in all aspects of their Division's operations. The rank of E-9 is a highly-prestigious position, and is not attained easily. Members must commit to a lengthy career of service, along with involvement in non-gameplay tasks (aka paperwork) listed above.",
                IsActive = true
            },
            new PayGrade {
                ID = 14,
                PayGradeName = "E-8",
                PayGradeDisplayName = "E-8",
                PayGradeOrderBy = 14,
                PayGradeGroup = "Senior NCO",
                PayGradeDescriptionText = "E-8s serve as high-level advisers to larger organizational units within the Fleet. They are typically attached to Warfare Command units, and assist with the development of NCOs in their subordinate units - utilizing their superior skills attained over an extended career of service. E-8 Members can also be seen performing highly specialized roles in Offices or Departments of higher-level units, rather than serving as an adviser and representative of the enlisted personnel below them.",
                IsActive = true
            },
            new PayGrade {
                ID = 15,
                PayGradeName = "E-7",
                PayGradeDisplayName = "E-7",
                PayGradeOrderBy = 15,
                PayGradeGroup = "Senior NCO",
                PayGradeDescriptionText = "E-7 is the first of the Senior NCO Ranks, and is normally the highest enlisted rank held by members deployed in operations within a Squadron or Platoon. E-7 denotes a Member's mastery of their Unit's skill-set, and their consistent success as leaders and teachers of subordinate NCOs and Members. As is the case with E-6, E-7 Members are seen performing the role of Assistant Squadron/Platoon Leader, but are also eligible for assignment to Air Wing and Combat Team units as a senior advisor to the Commanding Officer. E-7 Members can also be seen performing highly specialized roles for higher units, offices, and departments, rather than serving as an adviser and representative of the enlisted personnel below them.",
                PayGradeNotes = "E-6 Members who have been offered, but have declined a Commission to Officer Status will automatically be advanced to E-7 upon completion of TIS/TIG requirements listed below, bypassing any required approval from Division and Fleet HQs.",
                IsActive = true
            },
            //NCOs
            new PayGrade {
                ID = 16,
                PayGradeName = "E-6",
                PayGradeDisplayName = "E-6",
                PayGradeOrderBy = 16,
                PayGradeGroup = "NCO",
                PayGradeDescriptionText = "E-6 is the highest of the NCO Ranks. Members who have attained this PayGrade have achieved a high-level of qualification in the skills needed for their Unit's task(s), and have proven to be effective leaders of sub-units (Flights and Teams) in multiple operations. E-6 Members are typically seen performing the role of Assistant Squadron or Platoon Leader, working closely with the Unit's CO on duties such as personnel training, unit branding / public relations, recruitment, and operational planning.",
                PayGradeNotes = "In periods of Fleet re-structuring due to expansion, E-6 Members may be selected for a <strong>Commission</strong> to Officer Status (O-1) to begin their path towards commanding a newly created Unit of the same type.",
                IsActive = true
            },
            new PayGrade {
                ID = 17,
                PayGradeName = "E-5",
                PayGradeDisplayName = "E-5",
                PayGradeOrderBy = 17,
                PayGradeGroup = "NCO",
                PayGradeDescriptionText = "E-5 is the second of the NCO ranks, and is the required PayGrade to enter into a leadership position within a Permanent Unit. The Rank of E-5 signifies that a Member has attained a sufficient qualification in their Unit's performed task(s) to lead and develop the skills of other NCOs below them. E-5s are typically seen performing the role of Flight Leader or Squad Leader within Squadrons and Platoons, respectively. The rank of E-5 can be held by highly-skilled members of a unit who are not serving as a Flight or Squad Lead, but they will not be able to advance further without serving in a leadership position.",
                IsActive = true
            },
            new PayGrade {
                ID = 18,
                PayGradeName = "E-4",
                PayGradeDisplayName = "E-4",
                PayGradeOrderBy = 18,
                PayGradeGroup = "NCO",
                PayGradeDescriptionText = "E-4 is the first of the NCO Ranks. This rank signifies a Member's formal induction into a permanent Unit or Division, and the beginning of their qualification phase towards further promotion. E-4s are the backbone of the VVarMachine Fleet - they can perform any non-leadership Role within their Squadron or Platoon.",
                IsActive = true
            },
            //Enlisted
            new PayGrade {
                ID = 19,
                PayGradeName = "E-3",
                PayGradeDisplayName = "E-3",
                PayGradeOrderBy = 19,
                PayGradeGroup = "Member",
                PayGradeDescriptionText = "Advancement for a new Member from E-2 to E-3 can be attained immediately following the completion of the minimum two-week Vetting and Evaluation Process, after which the new Recruit becomes a full-fledged member of the VVAR Community and Fleet. Existing members of the VVarMachine Gaming Community who enlist in the Fleet will start at this rank.",
                PayGradeNotes = "NOTE: Because advancement beyond E-3 requires registration with a Division/Unit, many casual members will choose to stay at this rank.",
                IsActive = true
            },
            new PayGrade {
                ID = 20,
                PayGradeName = "E-2",
                PayGradeDisplayName = "E-2",
                PayGradeOrderBy = 20,
                PayGradeGroup = "Member",
                PayGradeDescriptionText = "All new Recruits to the VVarMachine Gaming Community and SC Fleet will start at this rank.",
                IsActive = true
            }
        };

        public static List<Rank> _ranks = new List<Rank>()
        {
            //Flag Officers
            new Rank
            {
                PayGradeID = 1,
                RankName = "Admiral",
                RankAbbr = "ADM",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O10.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 2,
                RankName = "Vice Admiral",
                RankAbbr = "VADM",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O9.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 3,
                RankName = "Rear Admiral",
                RankAbbr = "RADM",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O8.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 4,
                RankName = "Rear Admiral (Lower Half)",
                RankAbbr = "RDML",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 4,
                RankName = "Brigadier General",
                RankAbbr = "BGEN",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            //Senior Officers
            new Rank
            {
                PayGradeID = 5,
                RankName = "Captain",
                RankAbbr = "CAPT",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 5,
                RankName = "Colonel",
                RankAbbr = "Col",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 6,
                RankName = "Commander",
                RankAbbr = "CDR",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O5.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 6,
                RankName = "Lieutenant Colonel",
                RankAbbr = "LtCol",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O5.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 7,
                RankName = "Lieutenant Commander",
                RankAbbr = "LCDR",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O4.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 7,
                RankName = "Major",
                RankAbbr = "Maj",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O4.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            //Officers
            new Rank
            {
                PayGradeID = 8,
                RankName = "Lieutenant",
                RankAbbr = "LT",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 8,
                RankName = "Captain",
                RankAbbr = "Capt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 9,
                RankName = "Lieutenant (Junior Grade)",
                RankAbbr = "LTJG",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O2.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 9,
                RankName = "First Lieutenant",
                RankAbbr = "1stLt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O2.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 10,
                RankName = "Ensign",
                RankAbbr = "ENS",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O1.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 10,
                RankName = "Second Lieutenant",
                RankAbbr = "2ndLt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O1.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            //Officer Candidate
            new Rank
            {
                PayGradeID = 11,
                RankName = "Midshipman",
                RankAbbr = "MIDN",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/MIDN_4C.png",
                RankGroupName = "Officer Candidate",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/MIDN_4C.png",
                IsActive = true
            },
            //Senior NCO
            new Rank
            {
                PayGradeID = 12,
                RankName = "Fleet Master Chief Petty Officer",
                RankAbbr = "FMCPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E9_MCPON.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 13,
                RankName = "Master Chief Petty Officer",
                RankAbbr = "MCPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E9_GC.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 13,
                RankName = "Sergeant Major",
                RankAbbr = "SgtMaj",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E9-SGM.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 14,
                RankName = "Senior Chief Petty Officer",
                RankAbbr = "SCPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E8_GC.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 14,
                RankName = "First Sergeant",
                RankAbbr = "1stSgt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E8-1SG.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 15,
                RankName = "Chief Petty Officer",
                RankAbbr = "CPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 15,
                RankName = "Gunnery Sergeant",
                RankAbbr = "GySgt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E7.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            //NCOs
            new Rank
            {
                PayGradeID = 16,
                RankName = "Petty Officer 1st Class",
                RankAbbr = "PO1",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E6.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 16,
                RankName = "Staff Sergeant",
                RankAbbr = "SSgt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E6.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 17,
                RankName = "Petty Officer 2nd Class",
                RankAbbr = "PO2",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E5.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 17,
                RankName = "Sergeant",
                RankAbbr = "Sgt",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E5.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 18,
                RankName = "Petty Officer 3rd Class",
                RankAbbr = "PO3",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 18,
                RankName = "Corporal",
                RankAbbr = "Cpl",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E4.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            //Enlisted
            new Rank
            {
                PayGradeID = 19,
                RankName = "Airman",
                RankAbbr = "AN",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E3.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 19,
                RankName = "Lance Corporal",
                RankAbbr = "LCpl",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E3.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 20,
                RankName = "Airman Apprentice",
                RankAbbr = "AA",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 20,
                RankName = "Private 1st Class",
                RankAbbr = "PFC",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E2.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            }
        };
        #endregion

        #region OrgRoles
        public static List<OrgRole> _orgRoles = new List<OrgRole>()
        {
            //FleetCom
            new OrgRole {
                RoleOrderBy = 100,
                RoleName = "Fleet Commanding Officer",
                RoleShortName = "Fleet CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-10" } }
            },
            new OrgRole {
                RoleOrderBy = 101,
                RoleName = "Fleet Executive Officer",
                RoleShortName = "Fleet XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-9" } }
            },
            new OrgRole {
                RoleOrderBy = 102,
                RoleName = "Chief of Naval Operations",
                RoleShortName = "CNO",
                RoleDisplayName = "Chief of Naval Operations",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-8" } }
            },
            new OrgRole {
                RoleOrderBy = 105,
                RoleName = "Fleet Senior Enlisted Adviser",
                RoleShortName = "Fleet Adviser",
                RoleDisplayName = "Senior Enlisted Adviser",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-9 (Special)" } }
            },
            new OrgRole {
                RoleOrderBy = 106,
                RoleName = "Fleet Enlisted Adviser",
                RoleShortName = "Fleet Adviser",
                RoleDisplayName = "Enlisted Adviser",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-9" } }
            },

            //NAVCOM HQ
            new OrgRole {
                RoleOrderBy = 110,
                RoleName = "Naval Command Commanding Officer",
                RoleShortName = "NAVCOM CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-7" }, new PayGrade { PayGradeName = "O-6"} }
            },
            new OrgRole {
                RoleOrderBy = 111,
                RoleName = "Naval Command Executive Officer",
                RoleShortName = "NAVCOM XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-5" }, new PayGrade { PayGradeName = "O-6"} }
            },
            new OrgRole {
                RoleOrderBy = 112,
                RoleName = "Naval Command Senior Enlisted Adviser",
                RoleShortName = "NAVCOM Adviser",
                RoleDisplayName = "Senior Enlisted Adviser",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-9" } }
            },

            //AIR WING HQ
            new OrgRole {
                RoleOrderBy = 120,
                RoleName = "Air Wing Commanding Officer",
                RoleShortName = "Air Wing CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 121,
                RoleName = "Air Wing Executive Officer",
                RoleShortName = "Air Wing XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-3" }, new PayGrade { PayGradeName = "O-4"} }
            },
            new OrgRole {
                RoleOrderBy = 122,
                RoleName = "Air Wing Chief",
                RoleShortName = "Air Wing Chief",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-8" } }
            },

            //Combat Team HQ
            new OrgRole {
                RoleOrderBy = 130,
                RoleName = "Combat Team Commanding Officer",
                RoleShortName = "Combat Team CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 131,
                RoleName = "Combat Team Executive Officer",
                RoleShortName = "Combat Team XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-3" }, new PayGrade { PayGradeName = "O-4"} }
            },
            new OrgRole {
                RoleOrderBy = 132,
                RoleName = "Combat Team Sergeant",
                RoleShortName = "Team Sergeant",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-8" } }
            },

            //Squadron & Platoon Leadership
            new OrgRole {
                RoleOrderBy = 140,
                RoleName = "Squadron Leader",
                RoleShortName = "Squadron Lead",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-1" }, new PayGrade { PayGradeName = "O-2" }, new PayGrade { PayGradeName = "O-3"} }
            },
            new OrgRole {
                RoleOrderBy = 141,
                RoleName = "Platoon Leader",
                RoleShortName = "Platoon Lead",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-1" }, new PayGrade { PayGradeName = "O-2" }, new PayGrade { PayGradeName = "O-3"} }
            },
            new OrgRole {
                RoleOrderBy = 141,
                RoleName = "Officer-in-Training",
                RoleShortName = "OIT",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "OC" } }
            },
            new OrgRole {
                RoleOrderBy = 145,
                RoleName = "Squadron Chief",
                RoleShortName = "Squadron Chief",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-6" }, new PayGrade { PayGradeName = "E-7" } }
            },
            new OrgRole {
                RoleOrderBy = 146,
                RoleName = "Platoon Sergeant",
                RoleShortName = "Platoon Sgt.",
                RoleType = "UnitRole",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-6" }, new PayGrade { PayGradeName = "E-7" } }
            },


        };

        #endregion

        #region Units
        public static List<Unit> _units = new List<Unit>()
        {
            new Unit
            {
                UnitName = "Fleet Headquarters",
                ParentUnitName = null,
                UnitDesignation = "FLEETCOM",
                UnitDescription = @"Fleet Headquarters, also know as Fleet Command (FLEETCOM), is the command unit for the entire VVarMachine StarCitizen Fleet. Members of this unit and its reporting offices are responsible for the long-term strategic planning of Fleet Operations, Public Relations, coordination with other Organizations in the StarCitizen universe (including Alliance Member Fleet Organizations), and ongoing management of the Technology Resources serving the needs of VVarMachine personnel.",
                UnitCallsign = "Castle",
                UnitType = Model.Enums.UnitTypeEnum.Fleet,
                IsHidden = false,
                IsActive = true
            },

            //Naval Commands
            new Unit
            {
                UnitName = "Naval Air Forces Command",
                UnitDesignation = "NAVAIRFORCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Naval Air Forces Command (NAVAIRFORCOM) is the headquarters responsible for organizing and maintaining the Fleet's tactical air combat units. Officers and Senior NCOs assigned to this unit provide oversight in operational planning, mission execution, training, and ongoing management for the units under their authority. Each Air Wing within the Command is responsible for operations in a specific area of the air combat portfolio, and may include distinct methods for implementing the points listed above.",
                UnitCallsign = "Eagle",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Naval Logistics Command",
                UnitDesignation = "NAVLOGCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Naval Logistics Command (NAVLOGCOM) is the headquarters organizational unit for the Fleet's economic and combat support forces. It is responsible for planning and executing missions in commercial trade, mining, repair, salvage, exploration, and other critical roles to maintain our financial liquidity and ensure our Fleet can conduct future combat operations throughout the 'verse.",
                UnitCallsign = "Chronos",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Marine Forces Command",
                UnitDesignation = "MARFORCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Marine Forces Command (MARFORCOM) is the highest level of leadership for all Marine units of the VVarMachine Fleet. It is commanded by a Marine Colonel or Brigadier General, who is assisted by other Senior Officers and Senior NCOs. Together, they supervise the officers and enlisted personnel below them, and are responsible for oversight of all aspects of operational planning/execution, training, recruitment, discipline, and management of our Marines.",
                UnitCallsign = "Ares",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Naval Special Warfare Command",
                UnitDesignation = "NAVSPECWARCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Naval Special Warfare Command (NAVSPECWARCOM) was built to ensure the success of primary combat forces by means of the development and specialization of their supporting units, performing unconventional roles across the full range of military operations. Special Operations Capable (SOC) forces are not only those that conduct ""black"" missions or direct-action raids. They are front-line combat units who are relied upon by our Fleet's leadership to gather intelligence about the enemy, hinder their ability to respond, and work continually to test tactics and develop new SOPs for the remainder of the Fleet's forces to adopt.",
                UnitCallsign = "Chaos",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },

            //Air Wings
            new Unit
            {
                UnitName = "Fighter Wing 1",
                UnitDesignation = "1FW",
                ParentUnitName = "Naval Air Forces Command",
                UnitDescription = @"Fighter Wings are established to provide their Air Warfare Command with a organized leadership element for a collection of Fighter Squadrons. A Fighter Wing's subordinate squadrons are consistently responsible for performing the following roles during Fleet Operations:\r\n
- Space superiority for expeditionary combat operations\r\n
- Combat Air Patrol (CAP) of both secured and unsecured locations, including border protection\r\n
- Agile combat support for ComTrans, CSAR, EWAR, and strike units\r\n
- Escort and protection of various larger vessels",
                UnitCallsign = "Spartan",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Strike Wing 2",
                UnitDesignation = "2SW",
                ParentUnitName = "Naval Air Forces Command",
                UnitDescription = @"Strike Wing 2 was initially formed as a long-range bomber command to target Vanduul capital ships in the defense of Leir. In the recent years, based on the changing nature of the campaign, it has been expanded to include units in additional capabilities, primarily focused around support of various ground forces of the VVarMachine combined fleet. Units of Strike Wing 2 operate cutting-edge military spacecraft designed to punch above their weight class. Its versatile equipment manifest includes both carrier-based and land-based strike fighters, and heavy bomber craft of all sizes.",
                UnitCallsign = null,
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Special Operations Air Wing 3",
                UnitDesignation = "3SOAW",
                ParentUnitName = "Naval Special Warfare Command",
                UnitDescription = @"Units of Special Operations Air Wing 3 (SOAW 3) perform front-line combat tasking that falls outside the normal operating protocols of primary air forces. SOAW 3's Squadons are tasked with missions for Reconnaissance, Electronic Warfare, and direct-action raids against high value enemy targets. Although SOAW 3 is managed independently from primary air combat squadrons, it utilizes many of the same qualifications and evaluations processes for its members in different roles. In some cases, the requirements are much more diverse for squadrons under SOAW 3 when compared to the squadrons of NAVAIRFORCOM.",
                UnitCallsign = "Atlas",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Combat Support Wing 4",
                UnitDesignation = "4CSW",
                ParentUnitName = "Naval Air Forces Command",
                UnitDescription = @"Units of CSW 4 have been relied upon by primary air and ground forces in nearly every combat operation ever carried out by the fleet. These squadrons perform critical functions such as Combat Transport, and Combat Search & Rescue, where the primary responsibility of the unit is focused away from engaging and destroying enemy forces. While these units may not rack-up the kill count of primary combat forces, they provide an essential ingredient for mission success.",
                UnitCallsign = "Odin",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Air Mobility Wing 5",
                UnitDesignation = "5AMW",
                ParentUnitName = "Naval Logistics Command",
                UnitDescription = @"AMW 5 is comprised of units tasked with transportation of finished goods and supplies for our combat forces. Its Squadrons are constructed around dedicated, high-efficiency freighters of various sizes, categorized into two classifications by cargo capacity: Standard Airlift & Heavy Airlift.",
                UnitCallsign = "Nitro",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Air Resources Wing 6",
                UnitDesignation = "6ARW",
                ParentUnitName = "Naval Logistics Command",
                UnitDescription = @"ARW 6 was established with the focus of locating, extracting, and processing valuable natural resources. Every economic opportunity begins with discovery, and that discovery brings efforts to capitalize on newly found potential. With minimal logistical (transport) assistance, this Air Wing is capable of self-sufficient planning and execution of operations to obtain the varied resources on which our fleet depends.",
                UnitCallsign = "Blackjack",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },

            //Combat Teams
            new Unit
            {
                UnitName = "Infantry Combat Team 1",
                UnitDesignation = "ICT1",
                ParentUnitName = "Marine Forces Command",
                UnitDescription = @"Established in 2946, ICT 1 is the first of VVarMachine's multi-purpose organizational units of Marine Operators. Members of this unit and its child Platoons are primary front line forces, and are also trained in a wide variety of roles in unconventional and asymmetric warfare such as long range marksmanship, forward air control, close quarters combat, urban warfare, personnel extraction, demolition, and more. ICT 1 serves as MARFORCOM's primary assault force for missions requiring incursions into planetary or in-orbit installations. Additionally, in the absence of active tasking, ICT 1 is seen providing on-ship security for Command Elements and various commercial operations of Naval Logistics Command.",
                UnitCallsign = "Thunder",
                UnitType = Model.Enums.UnitTypeEnum.Team,
                IsHidden = false,
                IsActive = true
            },

            //Squadrons
            new Unit
            {
                UnitName = "112th Tactical Fighter Squadron",
                UnitDesignation = "VF-112",
                ParentUnitName = "Fighter Wing 1",
                UnitDescription = @"The 112th Fighter Squadron, or ""The Specter Knights"" as it is known within NAVAIRFORCOM, is one of VVarMachine's Space Superiority Squadrons. As part of the 1st Fighter Wing, the 112th is tasked with Space Warfare, Air Supremacy, and Agile Combat Support (ACS). Due to their wide range of capabilities, the 112th and her sister squadrons play a key role in all major combat operations. In addition to its primary responsibility as an Expeditionary Fighter Unit, the 112th may also be tasked with escorting corporate assets, providing perimeter and proximity security during mining operations, regular combat patrols, and securing the 142nd Combat Transport Squadron's Rapid Interstellar Mobility efforts. Whether a scalpel or a sledgehammer is needed, the 112th is sure to get the job done.",
                UnitCallsign = "Specter",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "114th Fighter Interceptor Squadron",
                UnitDesignation = "VF-114",
                ParentUnitName = "Fighter Wing 1",
                UnitDescription = @"Lightning speed and agility are the first things that come to mind for the 114th Fighter Interceptor Squadron. No other unit within NAVAIRFORCOM can be activated and directed on target as quickly as the 114th. Tasked with Interceptor roles where controlling borders is essential, the 114th can be airborne and on station before other units are even climbing into their cockpits, eliminating targets with pinpoint accuracy and efficiency not capable by most other fighter squadrons. Additionally, the 114th performs regular combat patrols, escort missions, and is usually the first of VVarMachine's squadrons sortied for defense against Vanduul raids in and around the Leir system.",
                UnitCallsign = "Reaper",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "117th Heavy Fighter Squadron",
                UnitDesignation = "VF-117",
                ParentUnitName = "Fighter Wing 1",
                UnitDescription = @"The 117th Heavy Fighter Squadron is the trebuchet of VVarMachine's Naval Air Forces. Tasked with extended duration missions, the 117th is the longest reaching squadron in NAVAIRFORCOM, and can operate without support from capital vessels to strike deep into enemy lines. The key roles of this squadron include escorting commercial assets on routes where refueling is not an option, and long range CAP missions in which heavy firepower is essential to area denial before standard fighter operations can arrive.",
                UnitCallsign = "Guardian",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "121st Bomber Squadron",
                UnitDesignation = "VA-121",
                ParentUnitName = "Strike Wing 2",
                UnitDescription = @"The 121st Bomber Squadron specializes in extended-range, high-damage, surgical strikes against large enemy vessels and installations. This unit operates traditional heavy bomber equipment and relies on support from 1st Fighter Wing units to get into its Area of Operations, deliver its ordinance on-target, and get back home safely. The 121st has served with distinction against numerous Vanduul battle groups during VVarMachine's ongoing campaign surrounding the Leir system, earning multiple capital-ship kills to its name.",
                UnitCallsign = "Hammer",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "123rd Attack Squadron",
                UnitDesignation = "VFA-123",
                ParentUnitName = "Strike Wing 2",
                UnitDescription = @"When the job requires putting heavy ordinance danger-close to Marines on the ground or in space, the 123rd is the unit to call. Operating the latest in carrier-based and land-based attack craft, the 123rd performs Close-Air-Support for troops deployed in a multitude of operational environments and scenarios. Unlike the 121st and other heavy bombardment squadrons, the 123rd conducts most of its operations in direct support of Marine forces, working closely with their Forward Air Controllers to suppress enemy ground forces and neutralize their defenses. Additionally, this Squadron may also be tasked as a supplemental strike force against medium size naval vessels including Corvettes, Frigates, and Transports during expeditionary and point defense operations.",
                UnitCallsign = "Madman",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "141st Rescue Squadron",
                UnitDesignation = "VCM-141",
                ParentUnitName = "Combat Support Wing 4",
                UnitDescription = @"The 141st Rescue Squadron specializes in terrestrial and interstellar Combat Search and Rescue (CSAR) missions in support of various operations of the VVarMachine Fleet, and will generally sortie all-purpose Rescue Flights centered around the highly capable Drake Cutlass Red Ambulance. In the absence of active tasking orders, the 141st is on constant standby for deep-space rescue to support the ongoing needs of all VVAR forces.",
                UnitCallsign = "Savior",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "142nd Combat Transport Squadron",
                UnitDesignation = "VC-142",
                ParentUnitName = "Combat Support Wing 4",
                UnitDescription = @"The 142nd Combat Transport Squadron focuses on bringing the troops and the tools needed to win the fight. Whether it is on a space station controlled by outlaws, on a capital ship in the middle of a fleet battle, or on a distant planet, this unit can be relied upon to get what we need, where we need it, when we need it. In addition to transporting troops, supplies, and vehicles for combat operations, this unit also provides on-demand fire support to Marine forces during insertions and extractions. This unit consistently works closely with other VVAR Military Units towards the success of the mission at-hand. This profession requires the most cool headed and daring pilots since the costs of failure are too high to incur.",
                UnitCallsign = "Blackwall",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "131st Reconnaissance Squadron",
                UnitDesignation = "VFR-131",
                ParentUnitName = "Special Operations Air Wing 3",
                UnitDescription = @"131st Reconnaissance Squadron is one of VVarMachine's Special-Operations Capable forces that provides essential elements of military intelligence to the command element of Naval Special Warfare Command (NAVSPECWARCOM); supporting task force commanders and their subordinate operating units of the Fleet Headquarters (FLEETCOM). 131st Reconnaissance Squadron conducts deep-space, long-range reconnaissance (GreenOps) and direct action raids (BlackOps) in support of VVarMachine's Military Division across the range of military operations including crisis response, expeditionary operations and major combat operations. The unit directly enhances the capabilities of primary air and ground forces through intelligence gathering, target designation, and precision strikes while utilizing methods of airborne, orbital, interstellar, and EVA insertions and extractions, similar to those of the UEE Marine Hell-Jumpers and Alliance Special Forces.",
                UnitCallsign = "Dagger",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "134th Electronic Attack Squadron",
                UnitDesignation = "VAQ-134",
                ParentUnitName = "Special Operations Air Wing 3",
                UnitDescription = @"The 134th Electronic Attack Squadron (134 EWAR) conducts operations aimed at removing the enemy's ability to ""fight smart"". Its pilots utilize a combination of deception tactics and anti-electronics technology to neutralize enemy sensors and defenses prior to a larger assault, with the goal of minimizing the loss of VVAR's primary combat forces. Primary tasking for this unit also includes conventional attacks against medium and large enemy spacecraft, Suppression of Enemy Air Defenses (SEAD), and Close Air Support. Additionally, the 134th is seen assisting the 131st Reconnaissance Squadron by operating the Drake Herald and Saber Raven for high-speed transfer of critical military intelligence from the front lines back to Command and Fleet HQ personnel during extended duration deep-space recon missions.",
                UnitCallsign = "Neptune",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "153rd Airlift Squadron",
                UnitDesignation = "VL-153",
                ParentUnitName = "Air Mobility Wing 5",
                UnitDescription = @"The 153d Airlift Squadron, activated Oct 8, 2947, is tasked with cargo and personnel transport on ships with less than 1000 SCU cargo capacity. Smaller cargo loads often imply more remote, valuable, urgent, or dangerous shipments than on larger freighters. This squadron’s personnel take pride in taking whatever, whenever, wherever, no matter the risk.",
                UnitCallsign = "Clipper",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "158th Heavy Airlift Squadron",
                UnitDesignation = "VL-158",
                ParentUnitName = "Air Mobility Wing 5",
                UnitDescription = @"The 158th Heavy Airlift Squadron, activated Oct 8, 2947, is the powerhouse behind any massive project; flying airframes that dwarf small stations, farther than thought possible every time. Capital ship delivery, station resupply, trade goods, and moving troops across galaxies with minimal stops are only some of the essential in-demand jobs this squadron performs in support of our Fleet.",
                UnitCallsign = "Husky",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "160th Mining Squadron",
                UnitDesignation = "VU-160",
                ParentUnitName = "Air Resources Wing 6",
                UnitDescription = @"The 160th Mining Squadron, activated 8 October 2947, is the backbone of VVarMachine’s economy. Operating craft big and small, drilling asteroids from the center of the Sol system, to the edges of the known universe, near the UEE or pirates and aliens, but always looking for the most valuable resources. With the dangers present, no miner is a civilian.",
                UnitCallsign = null,
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "162nd Exploration Squadron",
                UnitDesignation = "VP-162",
                ParentUnitName = "Air Resources Wing 6",
                UnitDescription = @"The 162nd Exploration Squadron, activated 8 October 2947, is composed of the exemplary troops mapping the galaxy. Its primary standing mission is to identify and report on new economic opportunities for the fleet. Heading places rarely trekked, explorers can also provide valuable information to create new charts and flight paths, and test existing paths for our fleet to traverse. While not tasked with infiltration, explorers still must expect combat and be properly trained beyond normal expectations in defense and escape, with the potential for no support to ever come.",
                UnitCallsign = "Patriot",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },

            //Marine Platoons
            new Unit
            {
                UnitName = "1st Platoon",
                UnitFullName = "1st Platoon, Infantry Combat Team 1",
                UnitDesignation = "ICT1-1",
                ParentUnitName = "Infantry Combat Team 1",
                UnitDescription = null,
                UnitCallsign = "Thunder 1",
                UnitType = Model.Enums.UnitTypeEnum.Platoon,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "2nd Platoon",
                UnitFullName = "2nd Platoon, Infantry Combat Team 1",
                UnitDesignation = "ICT1-2",
                ParentUnitName = "Infantry Combat Team 1",
                UnitDescription = null,
                UnitCallsign = "Thunder 2",
                UnitType = Model.Enums.UnitTypeEnum.Platoon,
                IsHidden = false,
                IsActive = true
            },


        };

        #endregion

    }
}
