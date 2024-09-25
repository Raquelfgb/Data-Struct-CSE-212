using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following people and priorities: Jane(1), Sam(1), Carl(1). To ensure that people are added to the back of the queue,
    // check that people are dequeued in the order they were added (FIFO)
    // Expected Result: Jane, Sam, Carl
    // Defect(s) Found: None :D
    public void TestPriorityQueue_QueueInOrder()
    {
        var priorityQueue = new PriorityQueue();

        var jane = new Person("Jane", 1);
        var sam = new Person("Sam", 1);
        var carl = new Person("Carl", 1);
        Person[] expectedResults = [jane, sam, carl];

        priorityQueue.Enqueue("Jane", 1);
        priorityQueue.Enqueue("Sam", 1);
        priorityQueue.Enqueue("Carl", 1);

        for (int i = 0; i < expectedResults.Length; i++)
        {
            var person = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i].Name, person);
        }

    }

    [TestMethod]
    // Scenario: Create a queue with the following people and priorities: Jane(1), Sam(3), Carl(2). 
    // Check that the people are dequeued in the order of highest priority 
    // Expected Result: Sam, Carl, Jane
    // Defect(s) Found: Test expected Carl but returned Sam. Sam was not removed from the list during dequeue.
    // Edited PriorityQueue.cs line 34 to remove highest priority item. 
    // Now test is returning Carl when Sam is expected. Not all items in list were compared
    // Edited PriorityQueue.cs line 27 to check index up to the length of the queue rather than length - 1
    public void TestPriorityQueue_DequeueInOrderOfPriority()
    {
        var priorityQueue = new PriorityQueue();

        var jane = new Person("Jane", 1);
        var sam = new Person("Sam", 3);
        var carl = new Person("Carl", 2);
        Person[] expectedResults = [sam, carl, jane];

        priorityQueue.Enqueue("Jane", 1);
        priorityQueue.Enqueue("Sam", 3);
        priorityQueue.Enqueue("Carl", 2);

        for (int i = 0; i < expectedResults.Length; i++)
        {
            var person = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i].Name, person);
        }

    }

    [TestMethod]
    // Scenario: Create a queue with the following people and priorities: Jane(1), Sam(3), Carl(2), Ed(3). 
    // Check that the people with matching priority are dequeued as FIFO
    // Expected Result: Sam, Ed, Carl, Jane
    // Defect(s) Found: Expected Sam but returned Ed which indicates that last item added of highest priority is being returned instead of first item with highest priority
    // Edited PriorityQueue.cs line 29 to only replace index when priority is > not >=
    public void TestPriorityQueue_MatchingPriority()
    {
        var priorityQueue = new PriorityQueue();

        var jane = new Person("Jane", 1);
        var sam = new Person("Sam", 3);
        var carl = new Person("Carl", 2);
        var ed = new Person("Ed", 3);
        Person[] expectedResults = [sam, ed, carl, jane];

        priorityQueue.Enqueue("Jane", 1);
        priorityQueue.Enqueue("Sam", 3);
        priorityQueue.Enqueue("Carl", 2);
        priorityQueue.Enqueue("Ed", 3);

        for (int i = 0; i < expectedResults.Length; i++)
        {
            var person = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i].Name, person);
        }

    }

    // Add more test cases as needed below.
    [TestMethod]
    // Scenario: Try to get the next person from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message.
    // Defect(s) Found: none found :D
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}