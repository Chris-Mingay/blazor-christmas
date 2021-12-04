<script>
	import { createEventDispatcher } from 'svelte';

	export let option;
	export let correctId;
	export let selected;

	const dispatch = createEventDispatcher();

	function onClick(){
		dispatch('clicked',option);
	}
</script>


<style lang='postcss'>
    .neutral {
        @apply bg-gray-200 text-gray-600;
    }
    .correct {
        @apply bg-green-500 text-white;
    }
    .correct-not-selected {
        @apply bg-green-100 text-green-300;
    }
    .incorrect {
        @apply bg-red-500 text-white;
    }
</style>


<button class='rounded-xl flex items-center justify-center font-semibold p-4'
				class:neutral="{!selected}"
				class:correct="{selected && correctId === option.id}"
				class:correct-not-selected="{!selected && correctId === option.id}"
				class:incorrect="{selected && correctId && correctId !== option.id}"
				on:click={onClick}
>
	{option.text}
</button>