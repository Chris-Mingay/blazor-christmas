<script context="module" lang="ts">
	export const prerender = true;
</script>

<script lang="ts">
	import AuthGuard from '$lib/AuthGuard.svelte';
	import { onMount } from 'svelte';
	import { browserSet, get } from '$lib/req_utils';
	import { isAuthenticated } from '../stores/authentication';
	import { goto } from '$app/navigation';
	import Redirect from '$lib/Redirect.svelte';

	let questions;

	onMount(getCalendar);

	async function getCalendar(){
		try {
			const json = await get(fetch, 'https://localhost:5021/api/Questions');
			console.log(json);
			if(json.succeeded){
				questions = json.data;
			}
		} catch (err){
			console.log(err);
		}

	}

	function questionClass(question){
		if(question.published) return 'ready';
		return '';
	}

	function gotoQuestion(question){
		if(!question.published) return;
		goto(`/questions/${question.id}`);
	}

</script>

<style lang='postcss'>
	.day {
			@apply flex items-center justify-center rounded-full bg-gray-200 py-2 font-semibold;
	}
	.header {
			@apply bg-white text-gray-700 mb-4;
	}
	.last-month, .after-christmas {
			@apply text-gray-300 bg-gray-100 cursor-not-allowed;
	}

	.this-month {
      @apply bg-blue-100 text-blue-300 cursor-not-allowed;
	}
  .correct {
      @apply bg-green-500 text-white cursor-pointer;
  }
  .incorrect {
      @apply bg-red-500 text-white cursor-pointer;
  }
	.ready {
      @apply bg-blue-500 text-white cursor-pointer animate-pulse;
	}
</style>

<svelte:head>
	<title>Christmas Quiz</title>
</svelte:head>

{#if questions}
	<div class='bg-white rounded-2xl overflow-hidden text-gray-800 w-3/4 lg:w-96 mx-auto shadow-lg'>
		<div class='p-4 bg-gray-100'>
			<h1 class='text-4xl font-bold text-center text-gray-600'>December</h1>
		</div>
		<div class='p-4 bg-white'>
			<div class='grid grid-cols-7 gap-2'>
				<div class='day header'>Mon</div>
				<div class='day header'>Tue</div>
				<div class='day header'>Wed</div>
				<div class='day header'>Thu</div>
				<div class='day header'>Fri</div>
				<div class='day header'>Sat</div>
				<div class='day header'>Sun</div>
				<div class='day last-month'>29</div>
				<div class='day last-month'>30</div>
				{#each questions as question}
					<div class='day this-month {questionClass(question)}' on:click={gotoQuestion(question)}>{question.dayNumber}</div>
				{/each}
				<div class='day after-christmas'>26</div>
			</div>
		</div>

	</div>
{/if}

<AuthGuard>
	<svelte:fragment slot='authed'>
	</svelte:fragment>
	<svelte:fragment slot='not_authed'>
		<Redirect url='/login'/>
	</svelte:fragment>
</AuthGuard>
