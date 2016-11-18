package chapt1;

import org.junit.Test;

import static chapt1.IsUnique.hasAllUniqueChars;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

/**
 * Created by xiaodoli on 11/17/2016.
 */
public class IsAllUniqueTest {
    @Test
    public void testStringIsAllUniqueCharacters() {
        String input1 = "AliveIs_awesome";
        boolean isAllUnique = hasAllUniqueChars(input1);
        assertFalse(isAllUnique);

        String input2 = "abcAdE";
        isAllUnique = hasAllUniqueChars(input2);
        assertTrue(isAllUnique);

        isAllUnique = hasAllUniqueChars(input2, true);
        assertFalse(isAllUnique);
    }
}
