package chapt1;

import org.junit.Test;

import java.util.HashSet;

import static org.junit.Assert.assertTrue;

/**
 * Created by xiaodoli on 11/17/2016.
 */
public class PalindromePermutationTest {

    @Test
    public void TestPalindromeString() {
        String input = "TactCoa";
        HashSet<String> candidates = PalindromePermutation.getPalindromePermutation(input);
        assertTrue(candidates.size()>0);
        assertTrue(candidates.contains("tacocat"));
        assertTrue(candidates.contains("atcocta"));
    }
}
