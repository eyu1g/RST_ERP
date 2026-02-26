using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;
using LeadEntity = Lead.Domain.Entities.Lead;
using LeadActivityEntity = Lead.Domain.Entities.LeadActivity;
using LeadSourceEntity = Lead.Domain.Entities.LeadSource;
using LeadStatusEntity = Lead.Domain.Entities.LeadStatus;
using LeadStageEntity = Lead.Domain.Entities.LeadStage;
using LeadAssignmentRuleEntity = Lead.Domain.Entities.LeadAssignmentRule;
using LeadScoreHistoryEntity = Lead.Domain.Entities.LeadScoreHistory;
using LeadDuplicateCandidateEntity = Lead.Domain.Entities.LeadDuplicateCandidate;
using LeadMergeLogEntity = Lead.Domain.Entities.LeadMergeLog;
using LeadConversionLogEntity = Lead.Domain.Entities.LeadConversionLog;
using LeadGroupEntity = Lead.Domain.Entities.LeadGroup;
using LeadGroupConditionEntity = Lead.Domain.Entities.LeadGroupCondition;

namespace Lead.Utility.Persistence;

public sealed class LeadDbContext : DbContext, ILeadDbContext
{
    public LeadDbContext(DbContextOptions<LeadDbContext> options) : base(options)
    {
    }

    public DbSet<LeadEntity> Leads => Set<LeadEntity>();
    public DbSet<LeadActivityEntity> LeadActivities => Set<LeadActivityEntity>();
    public DbSet<LeadSourceEntity> LeadSources => Set<LeadSourceEntity>();
    public DbSet<LeadStatusEntity> LeadStatuses => Set<LeadStatusEntity>();
    public DbSet<LeadStageEntity> LeadStages => Set<LeadStageEntity>();
    public DbSet<LeadAssignmentRuleEntity> LeadAssignmentRules => Set<LeadAssignmentRuleEntity>();
    public DbSet<LeadScoreHistoryEntity> LeadScoreHistories => Set<LeadScoreHistoryEntity>();
    public DbSet<LeadDuplicateCandidateEntity> LeadDuplicateCandidates => Set<LeadDuplicateCandidateEntity>();
    public DbSet<LeadMergeLogEntity> LeadMergeLogs => Set<LeadMergeLogEntity>();
    public DbSet<LeadConversionLogEntity> LeadConversionLogs => Set<LeadConversionLogEntity>();

    public DbSet<LeadGroupEntity> LeadGroups => Set<LeadGroupEntity>();
    public DbSet<LeadGroupConditionEntity> LeadGroupConditions => Set<LeadGroupConditionEntity>();
}
