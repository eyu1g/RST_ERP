using Microsoft.EntityFrameworkCore;
using LeadEntity = Lead.Domain.Entities.Lead;
using LeadActivityEntity = Lead.Domain.Entities.LeadActivity;
using LeadSourceEntity = Lead.Domain.Entities.LeadSource;
using LeadAssignmentRuleEntity = Lead.Domain.Entities.LeadAssignmentRule;
using LeadScoreHistoryEntity = Lead.Domain.Entities.LeadScoreHistory;
using LeadDuplicateCandidateEntity = Lead.Domain.Entities.LeadDuplicateCandidate;
using LeadMergeLogEntity = Lead.Domain.Entities.LeadMergeLog;
using LeadConversionLogEntity = Lead.Domain.Entities.LeadConversionLog;
using LeadStatusEntity = Lead.Domain.Entities.LeadStatus;
using LeadStageEntity = Lead.Domain.Entities.LeadStage;
using LeadIndustryEntity = Lead.Domain.Entities.LeadIndustry;
using LeadScoringRuleEntity = Lead.Domain.Entities.LeadScoringRule;
using LeadRoutingRuleEntity = Lead.Domain.Entities.LeadRoutingRule;
using LeadRoutingRuleConditionEntity = Lead.Domain.Entities.LeadRoutingRuleCondition;
using LeadGroupEntity = Lead.Domain.Entities.LeadGroup;
using LeadGroupConditionEntity = Lead.Domain.Entities.LeadGroupCondition;

namespace Lead.App.Interfaces;

public interface ILeadDbContext
{
    DbSet<LeadEntity> Leads { get; }
    DbSet<LeadActivityEntity> LeadActivities { get; }
    DbSet<LeadSourceEntity> LeadSources { get; }
    DbSet<LeadStatusEntity> LeadStatuses { get; }
    DbSet<LeadStageEntity> LeadStages { get; }
    DbSet<LeadIndustryEntity> LeadIndustries { get; }
    DbSet<LeadScoringRuleEntity> LeadScoringRules { get; }
    DbSet<LeadRoutingRuleEntity> LeadRoutingRules { get; }
    DbSet<LeadRoutingRuleConditionEntity> LeadRoutingRuleConditions { get; }
    DbSet<LeadAssignmentRuleEntity> LeadAssignmentRules { get; }
    DbSet<LeadScoreHistoryEntity> LeadScoreHistories { get; }
    DbSet<LeadDuplicateCandidateEntity> LeadDuplicateCandidates { get; }
    DbSet<LeadMergeLogEntity> LeadMergeLogs { get; }
    DbSet<LeadConversionLogEntity> LeadConversionLogs { get; }

    DbSet<LeadGroupEntity> LeadGroups { get; }
    DbSet<LeadGroupConditionEntity> LeadGroupConditions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
