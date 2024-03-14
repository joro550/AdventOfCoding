package day1

import (
	"aoc/common"
	"testing"
)

func TestGettingNumbers(t *testing.T) {
	test("1abc2", 12, GetNumbers, t)
	test("pqr3stu8vwx", 38, GetNumbers, t)
	test("a1b2c3d4e5f", 15, GetNumbers, t)
	test("treb7uchet", 77, GetNumbers, t)
}

func TestGettingReplacedNumber(t *testing.T) {
	test(ReplaceStringForNumber("two1nine"), 29, GetNumbers, t)
	test(ReplaceStringForNumber("eightwothree"), 83, GetNumbers, t)
	test(ReplaceStringForNumber("abcone2threexyz"), 13, GetNumbers, t)
	test(ReplaceStringForNumber("xtwone3four"), 24, GetNumbers, t)
	test(ReplaceStringForNumber("4nineeightseven2"), 42, GetNumbers, t)
	test(ReplaceStringForNumber("zoneight234"), 14, GetNumbers, t)
	test(ReplaceStringForNumber("7pqrstsixteen"), 76, GetNumbers, t)
}

func TestReplacingString(t *testing.T) {
	test("one", "1", ReplaceStringForNumber, t)
	test("onetwo", "12", ReplaceStringForNumber, t)
	test("onetwo1", "121", ReplaceStringForNumber, t)
	test("onetwoone", "121", ReplaceStringForNumber, t)
}

func TestSumNumbers(t *testing.T) {
	values := []string{
		"1abc2",
		"pqr3stu8vwx",
		"a1b2c3d4e5f",
		"treb7uchet",
	}

	sum := SumNumbers(values)
	if sum != 142 {
		t.Errorf("Expected 142 but got %d when summign", sum)
	}
}

func TestSumReplacedNumbers(t *testing.T) {
	values := []string{
		"two1nine",
		"eightwothree",
		"abcone2threexyz",
		"xtwone3four",
		"4nineeightseven2",
		"zoneight234",
		"7pqrstsixteen",
	}

	sum := SumNumbersWithReplacment(values)
	if sum != 281 {
		t.Errorf("Expected 281 but got %d when summign", sum)
	}
}

func TestPuzzle1(t *testing.T) {
	lines, err := common.GetLines("input.txt")
	if err != nil {
		t.Errorf("could not open file %v", err)
	}

	sum := SumNumbers(lines)
	if sum != 54644 {
		t.Errorf("Expected 54644 but got %d when summing", sum)
	}
}

func TestPuzzle2(t *testing.T) {
	lines, err := common.GetLines("input.txt")
	if err != nil {
		t.Errorf("could not open file %v", err)
	}

	sum := SumNumbersWithReplacment(lines)
	if sum != 53348 {
		t.Errorf("Expected 53348 but got %d when summing", sum)
	}
}

func test[E int | string](startValue string, expected E, method func(string) E, t *testing.T) {
	actual := method(startValue)
	if actual != expected {
		t.Errorf("expected %v but got %v for %s", expected, actual, startValue)
	}
}
