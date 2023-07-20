using DG.Tweening;
using UnityEngine;

namespace _Workspace.Scripts.Floating_Text
{
    public class FloatingText : MonoBehaviour
    {
        public FloatingTextRequirements _requirements;
        
    }

    public class FloatingTextRequirements
    {
        public string Text { get; private set; }
        public Vector3 StartPosition{ get; private set; }
        public float Offset { get; private set; }
        public float Duration { get; private set; }
        public Color Color { get; private set; }

        public Sequence Sequence { get; private set; }

        public float Delay { get; private set; }

        public FloatingTextRequirements(string text, Vector3 startPosition, float offset, float duration, Color color,Transform transform,float delay)
        {
            this.Text = text;
            this.StartPosition = startPosition;
            this.Offset = offset;
            this.Duration = duration;
            this.Color = color;
            this.Sequence = BuildSequence(transform);
            this.Delay = delay;
        }

        private Sequence BuildSequence(Transform transform)
        {
            Sequence = DOTween.Sequence();
            Sequence.Join(transform.DOScale(1, Duration/3).From(0));
            Sequence.Append(transform.DOMoveY(StartPosition.y + Offset, Duration));
            Sequence.Append(transform.DOScale(0, Duration/3));
            return Sequence;
        }
    }

    public class FloatingTextRequirementsBuilder
    {
        private string text = "+10$";
        private Vector3 startPosition;
        private float offset = 30;
        private float duration=1;
        private Color color = Color.green;
        private Transform textTransform { get; set; }

        private float delay = 0;
        
        public FloatingTextRequirementsBuilder SetText(string text)
        {
            this.text = text;
            return this;
        }

        public FloatingTextRequirementsBuilder SetStartPosition(Vector3 startPosition)
        {
            this.startPosition = startPosition;
            return this;
        }
        
        public FloatingTextRequirementsBuilder SetOffset(float offset)
        {
            this.offset = offset;
            return this;
        }

        public FloatingTextRequirementsBuilder SetDuration(float duration)
        {
            this.duration = duration;
            return this;
        }
        
        public FloatingTextRequirementsBuilder SetColor(Color color)
        {
            this.color = color;
            return this;
        }

        public FloatingTextRequirementsBuilder SetTransform(Transform transform)
        {
            textTransform = transform;
            return this;
        }

        public FloatingTextRequirementsBuilder SetDelay(float delay)
        {
            this.delay = delay;
            return this;
        }

        public FloatingTextRequirements Build()
        {
            return new FloatingTextRequirements(text, startPosition, offset, duration, color,textTransform,delay);
        }
    }
}