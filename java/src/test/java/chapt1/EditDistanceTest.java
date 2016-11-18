package chapt1;

import org.junit.Assert;
import org.junit.Test;

/**
 * Created by xiaodoli on 11/17/2016.
 */
public class EditDistanceTest {

    @Test
    public void TestShiftByOne() {
        int distance = EditDistance.calculateEditDistance("pale", "ple");
        Assert.assertEquals(1, distance);

        distance = EditDistance.calculateEditDistance("pales", "pale");
        Assert.assertEquals(1, distance);

        distance = EditDistance.calculateEditDistance("pale", "bale");
        Assert.assertEquals(1, distance);

        distance = EditDistance.calculateEditDistance("pale", "bake");
        Assert.assertNotSame(1, distance);
    }
}
