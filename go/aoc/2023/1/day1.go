package day1

import (
	"strconv"
)

func ReplaceStringForNumber(value string) string {
	dictionary := map[string]string{
		"one":   "1",
		"two":   "2",
		"three": "3",
		"four":  "4",
		"five":  "5",
		"six":   "6",
		"seven": "7",
		"eight": "8",
		"nine":  "9",
	}
	returnValue := ""

	for i, v := range value {

		// if this is allready a number lets just add it and move on
		_, err := strconv.Atoi(string(v))
		if err == nil {
			returnValue += string(v)
			continue
		}

		// see if we can match any of the dicrionary values
		remainingLetter := len(value[i:])
		for k, v := range dictionary {
			length := len(k)

			if length > remainingLetter {
				continue
			}

			stringSlice := value[i : i+(length)]

			if stringSlice == k {
				returnValue += v
				break
			}
		}
	}

	return returnValue
}

func GetNumbers(value string) int {
	first, last := 0, 0

	for _, v := range value {
		num, err := strconv.Atoi(string(v))
		if err != nil {
			continue
		}

		if first == 0 {
			first = num
			last = num
		} else {
			last = num
		}
	}

	return first*10 + last
}

func SumNumbers(values []string) int64 {
	total := int64(0)

	for _, value := range values {
		total += int64(GetNumbers(value))
	}
	return total
}

func SumNumbersWithReplacment(values []string) int64 {
	total := int64(0)

	for _, value := range values {

		newValue := ReplaceStringForNumber(value)
		total += int64(GetNumbers(newValue))
	}
	return total
}
