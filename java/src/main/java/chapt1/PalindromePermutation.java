package chapt1;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;

/**
 * Created by xiaodoli on 11/17/2016.
 *
 * Given a string, write a function to check if it is a permutation of a palindrome.
 * A palindrome is a word or phrase that is the same forwards and backwards. A permutation
 * is a rearrangement of letters. The palindrome does not need to be limited to just dictionary
 * words.
 *
 * Example
 * Input:       TactCoa
 * Output:      True (permutations: "tacocat", "atcocta", etc)
 */
public class PalindromePermutation {

    public static HashSet<String> getPalindromePermutation(String input) {
        Map<Character, Integer> occurrences = new HashMap<>();
        for(char c : input.toCharArray()) {
            char copy = Character.toLowerCase(c);
            if(occurrences.containsKey(copy)) {
                occurrences.put(copy, occurrences.get(copy)+1);
            } else {
                occurrences.put(copy, 1);
            }
        }

        int oddCharCount = 0;
        char middle = '\u0000';
        for(char c : occurrences.keySet()) {
            if(occurrences.get(c) % 2 != 0) {
                oddCharCount++;
                middle=c;
            }
        }

        HashSet<String> results = new HashSet<>();
        if(oddCharCount>1) {
            return results;
        }

        if (oddCharCount ==1) {
            occurrences.remove(middle);
            HashSet<String> halves = buildPalindrome(occurrences, "");
            for(String half : halves) {
                results.add(half + middle + reverse(half));
            }
        } else {
            HashSet<String> halves = buildPalindrome(occurrences, "");
            for(String half : halves) {
                results.add(half + reverse(half));
            }
        }

        return results;
    }

    private static HashSet<String> buildPalindrome(Map<Character, Integer> occurrences, String seed) {
        HashSet<String> results = new HashSet<>();
        if(occurrences.size()==0) {
            results.add(seed);
            return results;
        }
        for(char c : occurrences.keySet()) {
            Map<Character, Integer> rest = removeTwo(occurrences, c);
            String newSeed = seed + c;
            results.addAll(buildPalindrome(rest, newSeed));
        }
        return results;
    }

    private static Map<Character, Integer> removeTwo(Map<Character, Integer> map, char charToRemove) {
        Map<Character, Integer> rest = new HashMap<>();
        for(char c : map.keySet()) {
            int freq = map.get(c);
            if(freq<=0) {
                continue;
            }
            if(c==charToRemove) {
                freq-=2;
                if(freq>0) {
                    rest.put(c, map.get(c) - 2);
                }
            } else {
                rest.put(c, map.get(c));
            }
        }
        return rest;
    }

    private static String reverse(String input) {
        StringBuilder sb = new StringBuilder();
        for(int i = input.length()-1; i>=0;i--) {
            sb.append(input.charAt(i));
        }
        return sb.toString();
    }
}
