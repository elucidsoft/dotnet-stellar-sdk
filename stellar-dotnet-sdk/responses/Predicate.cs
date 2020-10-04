using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class Predicate
    {
        [JsonProperty(PropertyName = "and")]
        public Predicate[] And { get; set; }
        
        [JsonProperty(PropertyName = "or")]
        public Predicate[] Or { get; set; }
        
        [JsonProperty(PropertyName = "not")]
        public Predicate Not {get; set; }
        
        [JsonProperty(PropertyName = "unconditional")]
        public bool Unconditional { get; set; }
        
        [JsonProperty(PropertyName = "abs_before")]
        public DateTimeOffset? AbsBefore { get; set; }
        
        [JsonProperty(PropertyName = "rel_before")]
        public long? RelBefore { get; set; }

        public ClaimPredicate ToClaimPredicate()
        {
            if (And != null && And.Length > 0)
            {
                var leftPredicate = And[0].ToClaimPredicate();
                var rightPredicate = And[1].ToClaimPredicate();
                return ClaimPredicate.And(leftPredicate, rightPredicate);
            }

            if (Or != null && Or.Length > 0)
            {
                var leftPredicate = Or[0].ToClaimPredicate();
                var rightPredicate = Or[1].ToClaimPredicate();
                return ClaimPredicate.Or(leftPredicate, rightPredicate);
            }

            if (Not != null)
            {
                var predicate = Not.ToClaimPredicate();
                return ClaimPredicate.Not(predicate);
            }

            if (Unconditional)
            {
                return ClaimPredicate.Unconditional();
            }

            if (AbsBefore != null)
            {
                return ClaimPredicate.BeforeAbsoluteTime(AbsBefore.Value);
            }

            if (RelBefore != null)
            {
                return ClaimPredicate.BeforeRelativeTime(RelBefore.Value);
            }
            
            throw new Exception("Invalid Predicate");
        }
    }
}