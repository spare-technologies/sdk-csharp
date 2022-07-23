#!/bin/bash
failed=$(cat out.json | jq -r ".TestRun.ResultSummary.Counters.failed")
if [[ $failed == "0" ]]; then
    executed=$(cat out.json | jq -r ".TestRun.ResultSummary.Counters.executed")
    passed=$(cat out.json | jq -r ".TestRun.ResultSummary.Counters.passed")
    if [[ "$executed" == "$passed" ]]; then
        exit 0
    else
        exit 1
    fi
else
    exit 1
fi