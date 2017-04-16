using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace DoLess.Commands.Tests
{
    [TestFixture]
    public class CommandsTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        [Test]
        public void CreateThrowsIfExecutionParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Command.CreateFromAction((Action)null));
            Assert.Throws<ArgumentNullException>(() => Command.CreateFromAction((Action<string>)null));
            Assert.Throws<ArgumentNullException>(() => Command.CreateFromTask((Func<Task>)null));
            Assert.Throws<ArgumentNullException>(() => Command.CreateFromTask((Func<string, Task>)null));
            Assert.Throws<ArgumentNullException>(() => Command.CreateFromTask((Func<CancellationToken, Task>)null));
            Assert.Throws<ArgumentNullException>(() => Command.CreateFromTask((Func<string, CancellationToken, Task>)null));
        }

        [Test]
        public void IsExecutingIsTrueForAsyncMethods()
        {
            var command = Command.CreateFromTask(() => Task.Delay(5000));

            command.Execute(null);

            command.IsExecuting.Should().BeTrue();
        }

        [Test]
        public void IsExecutingIsTrueForSyncMethods()
        {
            var command = Command.CreateFromAction(() => Thread.Sleep(5000));

            command.Execute(null);

            command.IsExecuting.Should().BeTrue();
        }

        [Test]
        public void CanExecuteIsFalse()
        {
            var command = Command.CreateFromAction(() => { }, () => false);

            command.CanExecute(null).Should().BeFalse();
        }

        [Test]
        public void CanExecuteIsTrue()
        {
            var command = Command.CreateFromAction(() => { }, () => true);

            command.CanExecute(null).Should().BeTrue();
        }

        [Test]
        public void CanExecuteCanChange()
        {
            bool canExecute = false;
            var command = Command.CreateFromAction(() => { }, () => canExecute);

            command.CanExecute(null).Should().BeFalse();

            canExecute = true;
            command.CanExecute(null).Should().BeTrue();
        }

        [Test]
        public void ShouldExecuteIfCanExecute()
        {
            var executed = false;
            var command = Command.CreateFromAction(() => executed = true, () => true);
            command.ExecuteAsync().Wait();
            executed.Should().BeTrue();
        }

        [Test]
        public void ShouldNotExecuteIfCanNotExecute()
        {
            var executed = false;
            var command = Command.CreateFromAction(() => executed = true, () => false);
            command.ExecuteAsync().Wait();
            executed.Should().BeFalse();
        }

        [Test]
        public void ShouldExecuteIfCanExecuteChanged()
        {
            var executed = false;
            bool canExecute = false;
            var command = Command.CreateFromAction(() => executed = true, () => canExecute);
            command.ExecuteAsync().Wait();
            executed.Should().BeFalse();
            canExecute = true;
            command.ExecuteAsync().Wait();
            executed.Should().BeTrue();
        }
    }
}
