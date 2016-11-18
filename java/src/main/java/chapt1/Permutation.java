package chapt1;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by xiaodoli on 11/17/2016.
 *
 * Given two strings, write a method to decide if one is permutation of the other
 */
public class Permutation {
    public static boolean checkPermutation(String input1, String input2) {
        if (input1 == null || input2==null || input1.length() != input2.length()) {
            return false;
        }

        Map<Character, Integer> occurrences = new HashMap<>();
        for (char c : input1.toCharArray()) {
            if(occurrences.containsKey(c)) {
                occurrences.put(c, occurrences.get(c) + 1);
            }
        }

        for(char c : input2.toCharArray()) {
            if(!occurrences.containsKey(c)) {
                return false;
            }
            int freq = occurrences.get(c);
            freq-=1;
            if(freq<0) {
                return false;
            }
            occurrences.put(c, freq);
        }

        for(char c : occurrences.keySet()) {
            if(occurrences.get(c) != 0) {
                return false;
            }
        }

        return true;
    }
}
