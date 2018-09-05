using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Test.Helpers.Data
{
    public static partial class SetupData
    {
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

    }
}
