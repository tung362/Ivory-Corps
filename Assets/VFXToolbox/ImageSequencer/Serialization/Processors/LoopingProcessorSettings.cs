namespace UnityEngine.VFXToolbox.ImageSequencer
{
    public class LoopingProcessorSettings : ProcessorSettingsBase
    {
        public AnimationCurve curve;
        public int syncFrame;
        public int outputSequenceLength;

        public override void Default()
        {
            curve = new AnimationCurve();
            syncFrame = 25;
            outputSequenceLength = 25;
        }
    }
}