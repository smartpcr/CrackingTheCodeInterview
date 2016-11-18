package chapt1;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

/**
 * Created by xiaodoli on 11/17/2016.
 */
public class PermutationTest {

    @Test
    public void TestIsPermutation() {
        String s1 = "abc def";
        String s2 = " deabcf";
        boolean isPermutation = Permutation.checkPermutation(s1, s2);
        assertTrue(isPermutation);

        s2 = "aabcdef";
        isPermutation = Permutation.checkPermutation(s1, s2);
        assertFalse(isPermutation);
    }
}
