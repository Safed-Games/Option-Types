using NUnit.Framework;

namespace SafedGames.Options.Tests.Runtime
{
    public sealed class OptionTests
    {
        private const int TestValue = 100;

        private Option<int> _optionUnderTest;

        [Test]
        public void IsSome_OnSome_ReturnsTrue()
        {
            SetOptionToSome();
            Assert.True(_optionUnderTest.IsSome);
        }

        [Test]
        public void IsSome_OnNone_ReturnsFalse()
        {
            SetOptionToNone();
            Assert.False(_optionUnderTest.IsSome);
        }

        [Test]
        public void IsNone_OnSome_ReturnsFalse()
        {
            SetOptionToSome();
            Assert.False(_optionUnderTest.IsNone);
        }

        [Test]
        public void IsNone_OnNone_ReturnsTrue()
        {
            SetOptionToNone();
            Assert.True(_optionUnderTest.IsNone);
        }

        [Test]
        public void Or_OnSome_ReturnsValue()
        {
            SetOptionToSome();
            Assert.That(_optionUnderTest.Or(1), Is.EqualTo(TestValue));
        }

        [Test]
        public void Or_OnNone_ReturnsIfNoneValue()
        {
            SetOptionToNone();
            const int ifNone = 50;
            Assert.That(_optionUnderTest.Or(ifNone), Is.EqualTo(ifNone));
        }

        [Test]
        public void Match_OnSome_ExecutesIfSome()
        {
            SetOptionToSome();
            var reached = false;
            _optionUnderTest.Match(
                ifSome: _ => { reached = true; },
                ifNone: () => { });

            Assert.True(reached);
        }

        [Test]
        public void Match_OnNone_ExecutesIfNone()
        {
            SetOptionToNone();
            var reached = false;
            _optionUnderTest.Match(
                ifSome: _ => { },
                ifNone: () => { reached = true; });

            Assert.True(reached);
        }

        [Test]
        public void OrDefault_OnSome_ReturnsValue()
        {
            SetOptionToSome();
            Assert.That(_optionUnderTest.OrDefault(), Is.EqualTo(TestValue));
        }

        [Test]
        public void OrDefault_OnNone_ReturnsDefaultValue()
        {
            SetOptionToNone();

            Assert.That(_optionUnderTest.OrDefault(), Is.EqualTo(default(int)));
        }

        [Test]
        public void OrThrow_OnSome_ReturnsValue()
        {
            SetOptionToSome();
            Assert.That(_optionUnderTest.OrThrow(), Is.EqualTo(TestValue));
        }

        [Test]
        public void OrThrow_OnNone_Throws()
        {
            SetOptionToNone();

            Assert.Throws<AccessingNoneException>(() =>
                {
                    _ = _optionUnderTest.OrThrow();
                });
        }

        [Test]
        public void OrThrow_WithMessage_OnSome_ReturnsValue()
        {
            SetOptionToSome();
            Assert.That(_optionUnderTest.OrThrow("Different error!"), Is.EqualTo(TestValue));
        }

        [Test]
        public void OrThrow_WithMessage_OnNone_Throws()
        {
            SetOptionToNone();

            Assert.Throws<AccessingNoneException>(() =>
                {
                    _ = _optionUnderTest.OrThrow("Different error!");
                },
                "Different error!");
        }

        private void SetOptionToSome()
        {
            _optionUnderTest = Some<int>.Of(TestValue);
        }

        private void SetOptionToNone()
        {
            _optionUnderTest = None<int>.Object;
        }
    }
}
