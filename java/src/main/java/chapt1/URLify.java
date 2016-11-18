package chapt1;

import java.util.*;

/**
 * Created by xiaodoli on 11/17/2016.
 *
 * Write a method to replace all spaces in a string with '%20'. You may assume that the string
 * has sufficient space at the end to hold the additional characters, and that you are given the "true"
 * length of the string. (Note: if implementing in Java, please use a character array so that you can
 * perform this operation in place)
 *
 * Example
 * Input:       "Mr John Smith    ", 13
 * Output:      "Mr%20John%20Smith"
 */
public class URLify {
    public static String replaceWhiteSpace(String input) {
        StringBuilder sb = new StringBuilder();
        for(char c : input.trim().toCharArray()) {
            if(c == ' ') {
                sb.append("%20");
            } else {
                sb.append(c);
            }
        }
        return sb.toString();
    }
}
