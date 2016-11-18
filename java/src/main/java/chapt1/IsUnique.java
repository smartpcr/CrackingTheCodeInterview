package chapt1;

/**
 * Created by xiaodoli on 11/17/2016.
 *
 * Implement an algorithm to determine if a string has all unique characters.
 * What if you cannot use additional data structures?
 */
public class IsUnique {

    public static boolean hasAllUniqueChars(String input, boolean ignoreCase) {
        boolean[] charFlags = new boolean[128];
        for(int i = 0; i < input.length(); i++) {
            char c = input.charAt(i);
            int idx = Character.getNumericValue(c);
            if(!ignoreCase) {
                idx = (int) c;
            }
            if(charFlags[idx]) {
                return false;
            }
            charFlags[idx]=true;
        }
        return true;
    }

    public static boolean hasAllUniqueChars(String input) {
        return hasAllUniqueChars(input, false);
    }

    public static void main(String[] args) {


    }
}
