<script>
	import { onMount } from 'svelte';
	import { get } from '$lib/req_utils.js';
	import { goto } from '$app/navigation';

	let joined = true;
	let memberships;

	onMount(getQuestions);

	async function getQuestions() {
		try {
			const json = await get(fetch, 'https://localhost:5021/api/leagues');
			console.log(json);
			if (json.succeeded) {
				memberships = json.data;
			}
		} catch (err) {
			console.log(err);
		}

	}

	function gotoLeague(membership){
		goto(`/leagues/${membership.leagueId}`);
	}

</script>


{#if memberships}
	<div class='bg-white rounded-2xl overflow-hidden text-gray-800 w-3/4 lg:w-108 mx-auto shadow-lg'>
		<div class='p-4 text-white bg-blue-500'>
			<h1 class='text-4xl font-bold'>Your Leagues</h1>
		</div>

		{#if memberships.length == 0}
			<div class='p-4'>
				<p>You haven't joined any leagues yet. :(</p>
			</div>
		{:else}
			<div class='grid grid-cols-6 divide-y'>
				{#each memberships as membership}
					<div class='col-span-4 pl-4 py-4 pr-2 font-semibold' on:click={gotoLeague(membership)}>{membership.leagueName}</div>
					<div class='pl-2 pr-4 py-4'>
						<span class='w-6 inline-block text-yellow-500 font-semibold text-right'>1st</span>
						of {membership.numberOfMembers}
					</div>
					<div class='pl-2 pr-4 py-4'>{membership.points}pts</div>
				{/each}
			</div>
		{/if}

	</div>
{/if}