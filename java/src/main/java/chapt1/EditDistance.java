package chapt1;

/**
 * Created by xiaodoli on 11/17/2016.
 */
public class EditDistance {
    public static int calculateEditDistance(String input1, String input2) throws IllegalArgumentException {
        if (input1 == null || input2 == null) {
            throw new IllegalArgumentException("input cannot be null");
        }

        int len1 = input1.length();
        int len2 = input2.length();
        int[][] dp = new int[len1 + 1][len2 + 1];
        for (int i = 0; i <= len1; i++) {
            dp[i][0] = i;
        }
        for (int j = 0; j <= len2; j++) {
            dp[0][j] = j;
        }

        // iterate though, and check last char
        for (int i = 0; i < len1; i++) {
            char c1 = input1.charAt(i);
            for (int j = 0; j < len2; j++) {
                char c2 = input2.charAt(j);

                if (c1 == c2) {
                    dp[i + 1][j + 1] = dp[i][j];
                } else {
                    int replace = dp[i][j] + 1;
                    int insert = dp[i][j + 1] + 1;
                    int delete = dp[i + 1][j] + 1;

                    int min = replace > insert ? insert : replace;
                    min = delete > min ? min : delete;
                    dp[i + 1][j + 1] = min;
                }

            }
        }

        return dp[len1][len2];
    }
}
