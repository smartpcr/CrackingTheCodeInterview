package chapt1;

import org.junit.Assert;
import org.junit.Test;

/**
 * Created by xiaodoli on 11/17/2016.
 */
public class URLifyTest {
    @Test
    public void TestReplaceWhiteSpace() {
        String input = "Mr John Smith    ";
        String expected = "Mr%20John%20Smith";
        String result = URLify.replaceWhiteSpace(input);
        Assert.assertEquals(expected, result);
    }
}
