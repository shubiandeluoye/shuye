using SkillModule.Core.Types;

namespace SkillModule.Core.Skills
{
    public class SkillContext
    {
        public object Source { get; set; }
        public object Target { get; set; }
        public Vector3Data Position { get; set; }
        public Vector3Data Direction { get; set; }
        public float[] Parameters { get; set; }

        public SkillContext() { }

        public SkillContext(object source)
        {
            Source = source;
        }

        public SkillContext WithTarget(object target)
        {
            Target = target;
            return this;
        }

        public SkillContext WithPosition(Vector3Data position)
        {
            Position = position;
            return this;
        }

        public SkillContext WithDirection(Vector3Data direction)
        {
            Direction = direction;
            return this;
        }

        public SkillContext WithParameters(params float[] parameters)
        {
            Parameters = parameters;
            return this;
        }
    }
} 