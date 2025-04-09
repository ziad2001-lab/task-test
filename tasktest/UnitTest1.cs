namespace tasktest
{
    public class FizzBuzzDetectorTests
    {
        
            private FizzBuzzDetector detector;

            [SetUp]
            public void Setup()
            {
                detector = new FizzBuzzDetector();
            }

            [Test]
            public void TestExampleInput()
            {
                string input = @"Mary had a little lamb Little lamb, little lamb Mary had a little lamb It's fleece was white as snow";

                var result = detector.GetOverlappings(input);

                Assert.AreEqual(9, result.Count);

            string expected = @"Mary had Fizz little Buzz
Fizz lamb, little Fizz
Buzz had Fizz little lamb
FizzBuzz fleece was Fizz as Buzz";
            string expectedNoSpaces = expected.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            string actualNoSpaces = result.OutputString.Replace(" ", "").Replace("\n", "").Replace("\r", "");

            Assert.AreEqual(expectedNoSpaces, actualNoSpaces);
        }

        [Test]
            public void TestNullInput()
            {
                Assert.Throws<System.ArgumentNullException>(() => detector.GetOverlappings(null));
            }

            [Test]
            public void TestShortInput()
            {
                Assert.Throws<System.ArgumentException>(() => detector.GetOverlappings("Hi all"));
            }

            [Test]
            public void TestOnlySymbols()
            {
                string input = "!@#$$%^&*()";
                var result = detector.GetOverlappings(input);
                Assert.AreEqual(input, result.OutputString);
                Assert.AreEqual(0, result.Count);
            }

            [Test]
            public void TestAllFizzBuzzWords()
            {
                string input = "abcde abc abcdef ghi";
                var result = detector.GetOverlappings(input);
                Assert.AreEqual("abcde abc Fizz ghi", result.OutputString);
                Assert.AreEqual(1, result.Count);
            }
        
    }
}