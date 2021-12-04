<script>
	import { page } from '$app/stores';
	import { onMount } from 'svelte';
	import { get } from '$lib/req_utils.js';

	let id = $page.params.id;
	let league;

	async function getLeague(){
		try {
			const json = await get(fetch, `https://localhost:5021/api/Leagues/${id}`);
			if(json.succeeded){
				league = json.data;
			}
		} catch (err){
			console.log(err);
		}
	}

	const nth = function(d) {
		if (d > 3 && d < 21) return 'th';
		switch (d % 10) {
			case 1:  return "st";
			case 2:  return "nd";
			case 3:  return "rd";
			default: return "th";
		}
	}

	const medalClass = function(index){
		switch(index){
			case 0:
				return 'bg-yellow-500 text-yellow-100';
			case 1:
				return 'bg-gray-400 text-gray-100';
			case 2:
				return 'bg-yellow-800 text-yellow-500';
			default:
				return 'text-gray-400 bg-gray-50';
		}
	}

	onMount(getLeague)
</script>

{#if league}
	<div class='bg-white rounded-2xl overflow-hidden text-gray-800 w-3/4 lg:w-108 mx-auto shadow-lg'>
		<div class='p-4 text-white bg-blue-500'>
			<h1 class='text-4xl font-bold'>{league.name}</h1>
		</div>

		{#if league.leagueMemberships.length == 0}
			<div class='p-4'>
				<p>No one has joined this league yet. :(</p>
			</div>
		{:else}
			<div class='grid grid-cols-8 divide-y'>
				{#each league.leagueMemberships as membership,index}
					<div class='pl-4 py-4 pr-2  font-semibold text-center text-sm'>
						<span class='mx-auto flex items-center justify-center {medalClass(index)} rounded-full w-8 h-8'>
							{(index+1)}{nth(index+1)}
						</span>
					</div>
					<div class='col-span-5 py-4 px-2 font-semibold'>{membership.name}</div>
					<div class='pl-2 pr-4 py-4'>{membership.points}pts</div>
					<div class='pl-2 pr-4 py-4'>{membership.totalAnswerTime.toFixed(2)}s</div>
				{/each}
			</div>
		{/if}

	</div>
{/if}