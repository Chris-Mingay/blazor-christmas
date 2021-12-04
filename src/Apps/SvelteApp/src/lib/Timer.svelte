<script>
    import { createEventDispatcher } from 'svelte';

    export let duration = 5;
    let timeRemaining = duration;
    export let height = '4';

    var timer;
    var updateInterval;

    var _timeStarted;
    var _timeEnding;

    export function startTimer(){

        clearCallbacks();

        updateInterval = setInterval(onUpdateTimeRemaining, 10);
        timer = setTimeout(onTimerEnd, duration * 1000);

        _timeStarted = (new Date()).getTime();
        _timeEnding = _timeStarted + duration * 1000;
        
    }

    export function stopTimer(){
        clearCallbacks();
    }

    export function cancelTimer(){
        clearCallbacks();
    }

    function onUpdateTimeRemaining(){
        timeRemaining = _timeRemaining();
    }

    function onTimerEnd(){
        timeUp();
        clearCallbacks();
    }

    function clearCallbacks(){
        clearTimeout(timer);
        clearTimeout(updateInterval);
    }

    function _timeRemaining(){
        return _timeEnding ? ( _timeEnding - (new Date()).getTime() ) / 1000 : duration;
    }

    const dispatch = createEventDispatcher();

    function timeUp() {
        dispatch('timeUp');
    }


</script>

<div class="bg-gray-300 h-{height} w-full" on:click={startTimer}>
    {#if timeRemaining > 0}
        <div class="bg-red-500 h-{height}" style="width:{(timeRemaining * 100 / duration)}%"/>
    {/if}
</div>