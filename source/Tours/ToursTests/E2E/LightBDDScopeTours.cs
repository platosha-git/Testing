using LightBDD.XUnit2;

[assembly: ClassCollectionBehavior(AllowTestParallelization = true)]
[assembly: LightBddScopeAttribute]

namespace ToursTests.E2E
{
    internal class LightBddScopeTours : LightBddScopeAttribute
    {
        protected override void OnSetUp()
        {
        }

        protected override void OnTearDown()
        {
        }
    }
}
