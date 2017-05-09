using Sample1.Models;

namespace Sample1.Business.Abstract
{
    public interface IMultiStepTaskManager
    {
        MultiStepTask Create();

        void Start(MultiStepTask multiStepTask);

        void Complete(MultiStepTask multiStepTask);

        void Cancel(MultiStepTask multiStepTask);
    }
}
