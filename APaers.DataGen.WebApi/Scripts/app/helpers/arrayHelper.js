Object.defineProperty(Array.prototype, "removeCount", {
    // Specify "enumerable" as "false" to prevent function enumeration
    enumerable: false,

    /**
    * Removes @count occurence of specified item from array
    * @this Array
    * @param itemToRemove Item to remove from array
    * @count {Number} item count to remove
    * @returns {Number} Count of removed items
    */
    value: function (itemToRemove, count) {
        if (isNaN(count))
            count = 1;
        if (count === 0)
            return 0;
        // Count of removed items
        var removeCounter = 0;

        // Iterate every array item
        for (var index = 0; index < this.length; index++) {
            // If current array item equals itemToRemove then
            if (this[index] === itemToRemove) {
                // Remove array item at current index
                this.splice(index, 1);

                // Increment count of removed items
                removeCounter++;

                // Decrement index to iterate current position 
                // one more time, because we just removed item 
                // that occupies it, and next item took it place
                index--;
            }
            if (removeCounter >= count)
                break;
        }

        // Return count of removed items
        return removeCounter;
    }
});

Object.defineProperty(Array.prototype, "removeFirst", {
    // Specify "enumerable" as "false" to prevent function enumeration
    enumerable: false,

    /**
    * Removes first occurence of specified item from array
    * @this Array
    * @param itemToRemove Item to remove from array
    * @returns {boolean} true if item was found and removed
    */
    value: function (itemToRemove) {
        return this.removeCount(itemToRemove, 1) === 1;
    }
});

Object.defineProperty(Array.prototype, "removeAll", {
    // Specify "enumerable" as "false" to prevent function enumeration
    enumerable: false,

    /**
    * Removes all occurences of specified item from array
    * @this Array
    * @param itemToRemove Item to remove from array
    * @returns {Number} Count of removed items
    */
    value: function (itemToRemove) {
        return this.removeCount(itemToRemove, Number.MAX_VALUE);
    }
});
