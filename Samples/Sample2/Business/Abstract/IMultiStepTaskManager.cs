using Sample2.Models;

namespace Sample2.Business.Abstract
{
    public interface IMultiStepTaskManager
    {
        MultiStepTask Create();

        void Start(MultiStepTask multiStepTask);

        void Complete(MultiStepTask multiStepTask);

        void Cancel(MultiStepTask multiStepTask);
    }
}
